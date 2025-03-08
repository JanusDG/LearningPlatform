-- sudo /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "123?GHUiop" -i db_setup.sql -C


IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'LearningPlatform')
BEGIN
    CREATE DATABASE LearningPlatform;
END;

-- Ensure we're using the LearningPlatform database
USE LearningPlatform;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Courses') AND type = N'U')
BEGIN
    CREATE TABLE Courses (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL
    );
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Users') AND type = N'U')
BEGIN
    CREATE TABLE Users (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL
    );
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'UserCourses') AND type = N'U')
BEGIN
    CREATE TABLE UserCourses (
        UserID INT NOT NULL,
        CourseID INT NOT NULL,
        FOREIGN KEY (UserID) REFERENCES Users(ID),
        FOREIGN KEY (CourseID) REFERENCES Courses(ID),
        PRIMARY KEY (UserID, CourseID)
    );
END;
