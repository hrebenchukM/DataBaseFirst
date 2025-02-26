CREATE DATABASE Books; 
GO

USE Books; 
GO



CREATE TABLE authors (
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(255)
);


CREATE TABLE books (
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(255),
    author_id INT,
    FOREIGN KEY (author_id) REFERENCES authors(id) ON DELETE CASCADE
);




	INSERT INTO authors (name)
VALUES 
('Alexandra Bracken'),
('Sigmund Freud'),
('Milton Erickson'),
('Gary Chapman');

	INSERT INTO books (name, author_id)
VALUES 
('The Darkest Minds', 1),
('The Darkest Minds. Unfading', 1),
('The Darkest Minds. In the rays of sunset', 1),
('Dark Legacy', 1),
('Psychoanalysis', 2),
('My voice will stay with you',3),
('5 Love Languages', 4);



