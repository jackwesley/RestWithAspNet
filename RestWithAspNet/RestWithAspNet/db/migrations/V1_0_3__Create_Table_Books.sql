CREATE TABLE `books` (
	`Id` INT AUTO_INCREMENT,
	`Author` longtext,
	`LaunchDate` DATETIME(6) NOT NULL,
	`Price` DECIMAL(65,2) NOT NULL,
	`Title` longtext,
	PRIMARY KEY(`Id`)
)
ENGINE=InnoDB
;
