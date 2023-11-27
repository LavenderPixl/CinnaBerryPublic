USE [anna54c95.SKOLE]
GO
CREATE TABLE UserTable (
userId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
userName varchar(30) NOT NULL UNIQUE,
password varchar(max) NOT NULL,
email varchar(320)
);

USE [anna54c95.SKOLE]
GO
CREATE TABLE MoviesWatched (
watchedId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
movieGUId varchar(1000) NOT NULL,
userId INT NOT NULL, 
planToWatch bit not NULL
FOREIGN KEY (userId) REFERENCES UserTable (userId) ON DELETE CASCADE
);

USE [anna54c95.SKOLE]
GO
CREATE TABLE SeriesWatched (
watchedId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
seriesGUId varchar(1000) NOT NULL,
seasonGUId varchar(1000) NOT NULL,
userId INT NOT NULL, 
planToWatch bit not NULL
FOREIGN KEY (userId) REFERENCES UserTable (userId) ON DELETE CASCADE
);

USE [anna54c95.SKOLE]
GO
CREATE TABLE Review (
reviewId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
movieGUId varchar(1000) NOT NULL,
userId INT NOT NULL,
reviewText VARCHAR(5000),
rating int check(rating >= 1 and rating <= 10),
FOREIGN KEY (userId) REFERENCES UserTable (userId) ON DELETE CASCADE
);

USE [anna54c95.SKOLE]
GO
CREATE TABLE MediaImgUrl (
imgUrlId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
GUId varchar(1000) NOT NULL UNIQUE,
indexNumber int,
imgUrl varchar(max) NOT NULL
);



