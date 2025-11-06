--
-- netChat.
-- Prepared SQL queries for 'Message' definition.
--


--
-- SELECT template for table `Message`
--
SELECT `author`, `text`, `timeStamp` FROM `Message` WHERE 1;

--
-- INSERT template for table `Message`
--
INSERT INTO `Message`(`author`, `text`, `timeStamp`) VALUES (?, ?, ?);

--
-- UPDATE template for table `Message`
--
UPDATE `Message` SET `author` = ?, `text` = ?, `timeStamp` = ? WHERE 1;

--
-- DELETE template for table `Message`
--
DELETE FROM `Message` WHERE 0;

