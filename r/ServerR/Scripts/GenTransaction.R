perturbe <- function(x)
{
  s <- round(abs(sin(x)*1000))
  set.seed(s)
  x * (1 - rnorm(1) * sin(x) *2)
}

GenTransaction <- function(n)
{
  if(!is.element("devtools", installed.packages()[,1])){
    install.packages("devtools")
  } 
  
  if(!is.element("RPostgres", installed.packages()[,1])  || !is.element("DBI", installed.packages()[,1]) ){
    devtools::install_github("RcppCore/Rcpp")
    devtools::install_github("rstats-db/DBI")
    devtools::install_github("rstats-db/RPostgres")
  } 
  
  if(!is.element("uuid", installed.packages()[,1])){
    install.packages("uuid")
  } 
  
  
  library(RPostgres)
  library(DBI)
  library(uuid)
  
  con <- dbConnect(RPostgres::Postgres()
                   , host='tristesse.lol'
                   , port='5433'
                   , dbname='bandit-nbs'
                   , user='bandit'
                   , password="orF9YuPWVajej5tC6cfiro94BoxrzsoE")
  
  #Récuperer les valeurs quantitatives pour leur moyenne et ecart-type
  res <- dbGetQuery(con, 'SELECT "ClientMonthlySalary","TransferredAmount" FROM "BD_OPER_TRANSAC"')
  mSalary <- mean(res$ClientMonthlySalary)
  sSalary <- sd(res$ClientMonthlySalary)
  mAmount <- mean(res$TransferredAmount)
  sAmount <-sd(res$TransferredAmount)
  
  
  
  #PostgreSQ
  #les vecteurs
  Vec.Bank <- c("bandit-donsaluste", "bandit-radinou", "bandit-picsou", "bandit-profit")
  vec.genre <- c("male","female")
  vec.maritalStatus <- c("single", "married", "widowed", "divorced", "cohabiting")
  Vec.MerchantActivity <- c("Brass Animals","Trading Cards","Dachshunds")
  Vec.AuthMethode <- c("OTP","SMS","MAIL")
  
  #Birthdate
  # Générer des années de naissance aléatoires entre 1970 et 2005
  annees <- sample(1930:2005, n, replace = TRUE)
  
  # Générer des mois de naissance aléatoires entre janvier et décembre
  mois <- sample(1:12, n, replace = TRUE)
  
  # Générer des jours de naissance aléatoires entre 1 et 28
  jours <- sample(1:28, n, replace = TRUE)
  
  # Générer des heures de naissance aléatoires entre 0 et 23
  heures <- sample(0:23, n, replace = TRUE)
  
  # Générer des minutes de naissance aléatoires entre 0 et 59
  minutes <- sample(0:59, n, replace = TRUE)
  
  # Générer des secondes de naissance aléatoires entre 0 et 59
  secondes <- sample(0:59, n, replace = TRUE)
  
  # Combinez les années, mois, jours, heures, minutes et secondes en un vecteur de dates-temps
  ClientBirthDate <- as.POSIXct(paste(annees, mois, jours, heures, minutes, secondes, sep = "-"), format = "%Y-%m-%d-%H-%M-%S")
  
  
  #Age
  #calculer la différences entre auj et les dates
  diff <- difftime(Sys.time(), ClientBirthDate, units = "days") 
  
  # Convertir la différence en années
  ClientAge <- as.numeric(diff)/365.25
  
  ClientAge <- round(ClientAge)
  
  #TransactionDate
  start_date <- as.POSIXct("2023-01-01-0-0-0", format="%Y-%m-%d-%H-%M-%S")
  end_date <- Sys.time()
  TransactionDate<- as.POSIXct(start_date + runif(n, 0, difftime(end_date, start_date, units = "secs")), origin="1970-01-01")
  TransactionDate <- as.data.frame(TransactionDate)
  
  
  Id <- matrix(UUIDgenerate(n = n),n,1)
  DebitBank <- factor(sample(Vec.Bank,n,replace = TRUE))
  CreditBank <- factor(sample(Vec.Bank,n,replace = TRUE))
  ClientId <- matrix(UUIDgenerate(n = n),n,1)
  ClientGender <- factor(sample(vec.genre,n,replace = TRUE))
  ClientBirthDate <- as.data.frame(ClientBirthDate)
  ClientAge <- as.data.frame(ClientAge)
  ClientMaritalStatus <- factor(sample(vec.maritalStatus,n,replace = TRUE))
  ClientMonthlySalary <- matrix(floor(rnorm(n,mSalary,sSalary)),ncol = 1)
  ClientMonthlySalary <- matrix(pmax(floor(apply(ClientMonthlySalary,1,perturbe)),0),ncol = 1)
  MerchantActivity <- factor(sample(Vec.MerchantActivity,n,replace = TRUE))
  AuthenticationMethod <- factor(sample(Vec.AuthMethode,n,replace = TRUE))
  TransferredAmount <- matrix(round(rnorm(n,mAmount,sAmount),digits = 2),ncol = 1)
  TransferredAmount <- matrix(abs(round(apply(TransferredAmount,1,perturbe),digits = 2)),ncol = 1)
  
  df <- data.frame(Id,DebitBank,CreditBank,ClientId,ClientGender,ClientBirthDate,ClientAge,ClientMaritalStatus,ClientMonthlySalary,TransactionDate,MerchantActivity,AuthenticationMethod,TransferredAmount)
  
  con <- dbConnect(RPostgres::Postgres()
                   , host='localhost'
                   , port='5432'
                  , dbname='testNBS'
                 , user='akdim'
                , password="root")
  
  
  dbWriteTable(con, "Transaction", df, append = TRUE, row.names = FALSE)
  #dbWriteTable(con, "BD_OPER_TRANSAC", df, append = TRUE, row.names = FALSE)
}



