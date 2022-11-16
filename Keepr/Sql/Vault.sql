-- Active: 1666719198340@@SG-mild-zebra-6116-6849-mysql-master.servers.mongodirector.com@3306@CodeWorks

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId varchar(255) NOT NULL COMMENT 'Account Id',
        name varchar(255) NOT NULL COMMENT 'Keep Name',
        description varchar(255) COMMENT 'Keep Description',
        img varchar(255) NOT NULL COMMENT 'Keep Image',
        isPrivate TINYINT DEFAULT 0,
        Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';