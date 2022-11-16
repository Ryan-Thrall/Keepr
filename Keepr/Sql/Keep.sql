-- Active: 1666719198340@@SG-mild-zebra-6116-6849-mysql-master.servers.mongodirector.com@3306@CodeWorks

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId varchar(255) NOT NULL COMMENT 'Account Id',
        name varchar(255) NOT NULL COMMENT 'Keep Name',
        description TEXT COMMENT 'Keep Description',
        img varchar(255) NOT NULL COMMENT 'Keep Image',
        views INT DEFAULT 0 COMMENT 'Number of Views',
        kept INT DEFAULT 0 COMMENT 'Number of Vaults This Keep is in',
        Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT * FROM keeps;