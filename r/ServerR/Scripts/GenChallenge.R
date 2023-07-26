GenChallenge <- function(n)
{
  
  if(!is.element("devtools", installed.packages()[,1])){
    install.packages("devtools")
  } 
  
  if(!is.element("mongolite", installed.packages()[,1]) ){
    install.packages("mongolite", dependencies = T)
  } 
  
  if(!is.element("uuid", installed.packages()[,1])){
    install.packages("uuid")
  } 
  
  
  library(mongolite)
  library(uuid)
  
  
  #connexion à la BD
  uri <- "mongodb://bandit:KQVYJnxcMXvQ5joLef524V97zRkMMT3N@tristesse.lol:27018"
  con = mongo(collection = "BD_OPER_PROC_AUTH",db = "bandit-nbs",url = uri)

  
  
  #MongoDB
  #les vecteurs
  Vec.Bank <- c("bandit-donsaluste", "bandit-radinou", "bandit-picsou", "bandit-profit")
  Vec.ChalType <- c("OTP","SMS","MAIL")
  vec.genre <- c("male","female")
  
  #Birthdate
    # Générer des années de naissance aléatoires entre 1970 et 2005
    annees <- sample(1950:2005, n, replace = TRUE)
    
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
    BirtDate <- as.POSIXct(paste(annees, mois, jours, heures, minutes, secondes, sep = "-"), format = "%Y-%m-%d-%H-%M-%S")
  
  #Age
    #calculer la différences entre auj et les dates
    diff <- difftime(Sys.time(), BirtDate, units = "days") 
    
    # Convertir la différence en années
    Age <- as.numeric(diff)/365.25
    
    Age <- round(Age)
    
  #RequestTime
    start_date <- as.POSIXct("2023-01-01-0-0-0", format="%Y-%m-%d-%H-%M-%S")
    end_date <- Sys.time()
    RequestTime <- as.POSIXct(start_date + runif(n, 0, difftime(end_date, start_date, units = "secs")), origin="1970-01-01")
    
  #ResponseTime
    mins <- as.difftime(runif(n,1,20), units = "mins")
    ResponseTime <- RequestTime + mins
    DecisionTime <- RequestTime + mins
    
  
  Id <- matrix(UUIDgenerate(n = n),n,1)
  ChallengeId <- matrix(UUIDgenerate(n = n),n,1)
  ChallengeType <- factor(sample(Vec.ChalType ,n,replace = TRUE))
  BankId <- factor(sample(Vec.Bank ,n,replace = TRUE))
  ClientId <- matrix(UUIDgenerate(n = n),n,1)
  BirtDate <- as.data.frame(BirtDate)
  Age <- as.data.frame(Age)
  Gender <- factor(sample(vec.genre ,n,replace = TRUE))
  RequestTime <- as.data.frame(RequestTime)
  AttemptCount <- matrix(floor(runif(n,min = 0, max = 4)),ncol = 1)
  MaxAttemptsReached <- vector()
  Decision <- vector()
  for(i in 1:length(AttemptCount))
  {
    if(AttemptCount[i] > 2)
    {
      MaxAttemptsReached[i] <- TRUE
      Decision[i] <- "Refused"
    }
    else
    {
      MaxAttemptsReached[i] <- FALSE
      Decision[i] <- "Accepted"
    }
  }
  ResponseTime <- as.data.frame(ResponseTime)
  DecisionTime <- as.data.frame(DecisionTime)
  
  df <- data.frame(ChallengeId,ChallengeType,BankId,ClientId,BirtDate,Age,Gender,RequestTime,AttemptCount,ResponseTime,Decision,MaxAttemptsReached,DecisionTime)

  con$insert(df)
  
}