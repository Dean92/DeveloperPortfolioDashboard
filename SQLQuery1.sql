SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';

INSERT INTO Users (Username, Email) VALUES ('testuser', 'test@example.com');
INSERT INTO Projects (Title, Description, GitHubUrl, Stars, UserId)
VALUES ('Sample Project', 'A test project', 'https://github.com/sample', 42, 1);

SELECT * FROM Users;
SELECT * FROM Projects;
SELECT * FROM Skills;

SELECT s.Name, u.Username
FROM Skills s
JOIN Users u ON s.UserId = u.Id;

SELECT u.Username, p.Title, s.Name
FROM Users u
LEFT JOIN Projects p ON u.Id = p.UserId
LEFT JOIN Skills s ON u.Id = s.UserId
WHERE u.Id = 1;


SELECT @@VERSION;