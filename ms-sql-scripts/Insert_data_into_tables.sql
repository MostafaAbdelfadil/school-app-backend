use School;

-- Insert into Class
SET IDENTITY_INSERT Class ON;
INSERT INTO Class (ClassId, ClassName) VALUES 
(1, 'Class A'),
(2, 'Class B'),
(3, 'Class C'),
(4, 'Class D'),
(5, 'Class E'),
(6, 'Class F'),
(7, 'Class G'),
(8, 'Class H'),
(9, 'Class I'),
(10, 'Class J'),
(11, 'Class K'),
(12, 'Class L');
SET IDENTITY_INSERT Class OFF;

-- Insert into Subject
SET IDENTITY_INSERT Subject ON;
INSERT INTO Subject (SubjectId, SubjectName, Description) VALUES 
(1, 'Mathematics', 'Covers algebra, geometry, and arithmetic concepts'),
(2, 'Science', 'Focus on physics, chemistry, and biology basics'),
(3, 'English', 'Covers reading, writing, and grammar skills'),
(4, 'History', 'Study of ancient and modern historical events'),
(5, 'Geography', 'Covers physical and political geography'),
(6, 'Art', 'Includes drawing, painting, and creative arts'),
(7, 'PhyEducation', 'Focus on fitness and sports activities');
SET IDENTITY_INSERT Subject OFF;

-- Insert into teacher
SET IDENTITY_INSERT Teacher ON;
INSERT INTO Teacher (TeacherId, FirstName, LastName, Email, Phone, Address, DateOfBirth, JoiningDate, SubjectId) VALUES 
(1, 'Ali', 'Hassan', 'ali.hassan@email.com', '0121001001', 'Cairo, Egypt', '1985-03-10', '2015-08-01', 1),
(2, 'Fatma', 'Ahmed', 'fatma.ahmed@email.com', '0122002002', 'Giza, Egypt', '1990-05-15', '2016-08-01', 2),
(3, 'Mahmoud', 'Omar', 'mahmoud.omar@email.com', '0123003003', 'Alexandria, Egypt', '1988-11-12', '2014-09-01', 3),
(4, 'Noura', 'Saad', 'noura.saad@email.com', '0124004004', 'Assiut, Egypt', '1986-07-30', '2017-08-01', 4),
(5, 'Hany', 'Nabil', 'hany.nabil@email.com', '0125005005', 'Ismailia, Egypt', '1983-06-20', '2013-08-01', 5),
(6, 'Dina', 'Fouad', 'dina.fouad@email.com', '0126006006', 'Luxor, Egypt', '1992-09-18', '2018-08-01', 6),
(7, 'Karim', 'Saleh', 'karim.saleh@email.com', '0127007007', 'Suez, Egypt', '1987-04-25', '2015-08-01', 7);
SET IDENTITY_INSERT Teacher OFF;

-- Insert into Student
SET IDENTITY_INSERT Student ON;
INSERT INTO Student (StudentId, FirstName, LastName, Email, Phone, Address, GenderId, DateOfBirth, EnrollDate, ClassId) VALUES 
(1, 'Ahmed', 'Hassan', 'ahmed.hassan@email.com', '0101001001', 'Cairo, Egypt', 1, '2008-05-10', '2020-09-01', 1),
(2, 'Mona', 'Fathy', 'mona.fathy@email.com', '0102002002', 'Giza, Egypt', 2, '2009-03-15', '2021-09-01', 2),
(3, 'Kareem', 'Saeed', 'kareem.saeed@email.com', '0103003003', 'Alexandria, Egypt', 1, '2007-11-20', '2019-09-01', 3),
(4, 'Sara', 'Mohamed', 'sara.mohamed@email.com', '0104004004', 'Assiut, Egypt', 2, '2010-07-22', '2022-09-01', 4),
(5, 'Youssef', 'Ali', 'youssef.ali@email.com', '0105005005', 'Ismailia, Egypt', 1, '2006-06-14', '2018-09-01', 5),
(6, 'Laila', 'Gamal', 'laila.gamal@email.com', '0106006006', 'Luxor, Egypt', 2, '2008-08-28', '2020-09-01', 6),
(7, 'Omar', 'Mostafa', 'omar.mostafa@email.com', '0107007007', 'Suez, Egypt', 1, '2009-01-18', '2021-09-01', 7),
(8, 'Hana', 'Abbas', 'hana.abbas@email.com', '0108008008', 'Fayoum, Egypt', 2, '2011-02-05', '2023-09-01', 8),
(9, 'Tamer', 'Nabil', 'tamer.nabil@email.com', '0109009009', 'Port Said, Egypt', 1, '2007-10-11', '2019-09-01', 9),
(10, 'Reem', 'Farid', 'reem.farid@email.com', '0110000000', 'Minya, Egypt', 2, '2010-04-25', '2022-09-01', 10),
(11, 'Ziad', 'Adel', 'ziad.adel@email.com', '0111001001', 'Qena, Egypt', 1, '2006-09-09', '2018-09-01', 11),
(12, 'Malak', 'Ashraf', 'malak.ashraf@email.com', '0112002002', 'Damietta, Egypt', 2, '2008-12-13', '2020-09-01', 12),
(13, 'Fady', 'Ramzy', 'fady.ramzy@email.com', '0113003003', 'Aswan, Egypt', 1, '2009-08-08', '2021-09-01', 1),
(14, 'Nada', 'El-Sayed', 'nada.elsayed@email.com', '0114004004', 'Beni Suef, Egypt', 2, '2011-06-23', '2023-09-01', 2),
(15, 'Mohab', 'Hussein', 'mohab.hussein@email.com', '0115005005', 'Beheira, Egypt', 1, '2007-03-04', '2019-09-01', 3);
SET IDENTITY_INSERT Student OFF;