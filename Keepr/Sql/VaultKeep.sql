-- Active: 1666719198340@@SG-mild-zebra-6116-6849-mysql-master.servers.mongodirector.com@3306@CodeWorks

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL COMMENT 'Account of The Saver to the vault',
        vaultId int NOT NULL COMMENT 'The Vault the Keep is Stored In',
        keepId int NOT NULL COMMENT 'The Keep that is being stored'
    ) default charset utf8 COMMENT '';

SELECT
    k.*,
    vk.*,
    vk.id as vkId,
    a.*
FROM vaultkeeps vk
    JOIN keeps k ON k.id = vk.keepId
    JOIN accounts a ON a.id = k.creatorId
WHERE vk.vaultId = 76;

select * FROM vaultkeeps;