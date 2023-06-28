CREATE DATABASE todolist CHARACTER SET utf8 COLLATE utf8_general_ci;

CREATE TABLE todos (
                       `TodoId` int AUTO_INCREMENT,
                       `Text` VARCHAR(255) NOT NULL DEFAULT "",
                       `Done` BOOLEAN NOT NULL DEFAULT FALSE,
                       PRIMARY KEY(`TodoID`));