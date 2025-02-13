CREATE DATABASE School
ON 
(
    NAME = SchoolDB_Data,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\School_Data.mdf',  -- Location for the data file
    SIZE = 10MB,  -- Initial size
    MAXSIZE = UNLIMITED,  -- Maximum size
    FILEGROWTH = 5MB  -- Growth size
)
LOG ON 

(
    NAME = SchoolDB_Log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\School_Log.ldf',  -- Location for the log file
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 2MB
);

use School;

-- lookup table of students gender
CREATE TABLE LookupGender (
    GenderId INT PRIMARY KEY,
    GenderName CHAR(1) NOT NULL UNIQUE
);


-- Create Class Table
CREATE TABLE Class (
    ClassId INT IDENTITY(1,1) PRIMARY KEY,
    ClassName VARCHAR(15) NOT NULL UNIQUE,
);

-- Create Subject Table
CREATE TABLE Subject (
    SubjectId INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName VARCHAR(15) NOT NULL UNIQUE,
    Description VARCHAR(max)
);

-- Create Student Table
CREATE TABLE Student
(
	StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(15) NOT NULL,
    LastName VARCHAR(15) NOT NULL,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Phone VARCHAR(15) NOT NULL UNIQUE,
	Address VARCHAR(50) NOT NULL,
	DateOfBirth DATE NOT NULL,
    EnrollDate DATE NOT NULL DEFAULT GETDATE(),
	GenderId INT NOT NULL REFERENCES LookupGender(GenderId),
	ClassId INT REFERENCES Class(ClassId) ON UPDATE CASCADE ON DELETE SET NULL
);

-- Create Teacher Table
CREATE TABLE Teacher
(
	TeacherId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(15) NOT NULL,
    LastName VARCHAR(15) NOT NULL,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Phone VARCHAR(15) NOT NULL UNIQUE,
	Address VARCHAR(50) NOT NULL,
	DateOfBirth DATE NOT NULL,
    JoiningDate DATE NOT NULL DEFAULT GETDATE(),
	SubjectId INT REFERENCES Subject(SubjectId) ON UPDATE CASCADE ON DELETE SET NULL
);




