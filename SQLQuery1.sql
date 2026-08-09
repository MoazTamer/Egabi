-- Create Database
CREATE DATABASE StudentManagementDB;

USE StudentManagementDB;
GO


-- Create Faculties Table
CREATE TABLE Faculties
(
    FacultyId INT IDENTITY(1,1) PRIMARY KEY,
    FacultyName NVARCHAR(30) NOT NULL
);


-- Create Students Table
CREATE TABLE Students
(
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentName NVARCHAR(60) NOT NULL,
    Age INT NOT NULL,
    GPA DECIMAL(3,2) NOT NULL,
    FacultyId INT NOT NULL,

    CONSTRAINT FK_Students_Faculties
        FOREIGN KEY (FacultyId)
        REFERENCES Faculties(FacultyId),

    CONSTRAINT CK_Students_Age
        CHECK (Age > 0),

    CONSTRAINT CK_Students_GPA
        CHECK (GPA BETWEEN 0 AND 4)
);


-- Create Courses Table
CREATE TABLE Courses
(
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(30) NOT NULL,
    Credits INT NOT NULL
);


-- Create Enrollments Table
CREATE TABLE Enrollments
(
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    EnrollmentDate DATE NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_Enrollments
        PRIMARY KEY (StudentId, CourseId),

    CONSTRAINT FK_Enrollments_Students
        FOREIGN KEY (StudentId)
        REFERENCES Students(StudentId),

    CONSTRAINT FK_Enrollments_Courses
        FOREIGN KEY (CourseId)
        REFERENCES Courses(CourseId)
);


-- Insert Faculties Data
INSERT INTO Faculties (FacultyName)
VALUES
('Computer Science'),
('Engineering'),
('Business'),
('Medicine');


-- Insert Students Data
INSERT INTO Students
    (StudentName, Age, GPA, FacultyId)
VALUES
    ('Ahmed Ali', 20, 3.50, 1),
    ('Mohamed Hassan', 21, 3.80, 1),
    ('Sara Ahmed', 19, 2.90, 1),
    ('Omar Khaled', 22, 3.20, 2),
    ('Mariam Adel', 21, 3.70, 2),
    ('Youssef Samir', 20, 2.60, 3),
    ('Nour Mohamed', 22, 3.40, 3),
    ('Hana Mahmoud', 23, 3.90, 4);


-- Insert Courses Data
INSERT INTO Courses
    (CourseName, Credits)
VALUES
    ('Database Systems', 3),
    ('Object Oriented Programming', 3),
    ('Web Development', 3),
    ('Computer Networks', 3),
    ('Software Engineering', 3);

    
-- Insert Enrollments  Data
INSERT INTO Enrollments
    (StudentId, CourseId)
VALUES
    (2, 1),
    (2, 2),
    (2, 3),
    (3, 1),
    (3, 2),
    (3, 5),
    (4, 1),
    (5, 1),
    (5, 4),
    (6, 2),
    (6, 4),
    (6, 5),
    (7, 3),
    (8, 1),
    (8, 3),
    (9, 5);



--- SELECT Queries
   
-- 1. Select All Students
SELECT *
FROM Students;


-- 2. Select All Faculties
SELECT *
FROM Faculties;


-- 3. Select Students with GPA >= 3
SELECT
    StudentId,
    StudentName,
    GPA
FROM Students
WHERE GPA >= 3;


-- 4. Select Students Ordered by GPA
SELECT
    StudentName,
    GPA
FROM Students
ORDER BY GPA DESC;


-- 5. Select Students from Computer Science Faculty
SELECT
    s.StudentName,
    s.GPA,
    f.FacultyName
FROM Students s
INNER JOIN Faculties f
    ON s.FacultyId = f.FacultyId
WHERE f.FacultyName = 'Computer Science';


---   Create 3 Business Reports using JOIN
   
-- 1. Students with Faculty Names
SELECT *
FROM Students s
INNER JOIN Faculties f
    ON s.FacultyId = f.FacultyId
ORDER BY f.FacultyName, s.StudentName;


-- 2. Courses with Number of Students
SELECT
    c.CourseId,
    c.CourseName,
    COUNT(e.StudentId) AS NumberOfStudents
FROM Courses c
LEFT JOIN Enrollments e
    ON c.CourseId = e.CourseId
GROUP BY
    c.CourseId,
    c.CourseName
ORDER BY NumberOfStudents DESC;


-- 3. Students and Their Enrolled Courses
SELECT
    s.StudentName,
    c.CourseName,
    e.EnrollmentDate
FROM Students s
INNER JOIN Enrollments e
    ON s.StudentId = e.StudentId
INNER JOIN Courses c
    ON e.CourseId = c.CourseId
ORDER BY
    s.StudentName,
    c.CourseName;


---  Create 2 Dashboard Reports using GROUP BY

-- 1. Students per Faculty
SELECT
    f.FacultyName,
    COUNT(s.StudentId) AS NumberOfStudents
FROM Faculties f
LEFT JOIN Students s
    ON f.FacultyId = s.FacultyId
GROUP BY f.FacultyName
ORDER BY NumberOfStudents DESC;


-- 2. Average GPA per Faculty
SELECT
    f.FacultyName,
    AVG(s.GPA) AS AverageGPA
FROM Faculties f
LEFT JOIN Students s
    ON f.FacultyId = s.FacultyId
GROUP BY f.FacultyName
ORDER BY AverageGPA DESC;


--   Create 1 Database View   
CREATE VIEW StudentSummaryView
AS
SELECT
    s.StudentId,
    s.StudentName,
    s.Age,
    s.GPA,
    f.FacultyId,
    f.FacultyName
FROM Students s
INNER JOIN Faculties f
    ON s.FacultyId = f.FacultyId;
GO


-- Select Data from StudentSummaryView
SELECT *
FROM StudentSummaryView;


--  Create 1 Stored Procedure

CREATE PROCEDURE GetStudentsByFaculty
    @FacultyId INT
AS
BEGIN
    SELECT
        s.StudentId,
        s.StudentName,
        s.Age,
        s.GPA,
        f.FacultyName
    FROM Students s
    INNER JOIN Faculties f
        ON s.FacultyId = f.FacultyId
    WHERE s.FacultyId = @FacultyId;
END;
GO


-- Execute Stored Procedure
EXEC GetStudentsByFaculty @FacultyId = 1;


--  Execution Plan

SELECT
    s.StudentName,
    s.GPA,
    f.FacultyName
FROM Students s
INNER JOIN Faculties f
    ON s.FacultyId = f.FacultyId
WHERE s.GPA >= 3;
