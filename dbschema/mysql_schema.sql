/* SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO"; */
/* SET AUTOCOMMIT = 0; */
/* START TRANSACTION; */
/* SET time_zone = "+00:00"; */

-- --------------------------------------------------------

--
-- Table structure for table `Error` generated from model 'Error'
--

CREATE TABLE IF NOT EXISTS `ERROR` (
  `Error` TEXT DEFAULT NULL COMMENT 'Not found error message'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Message` generated from model 'Message'
--

CREATE TABLE `Message` (
  `author` TEXT DEFAULT NULL COMMENT 'The author of the message',
  `text` TEXT DEFAULT NULL COMMENT 'The text contents of the message',
  `timeStamp` DATETIME DEFAULT NULL COMMENT 'Time the message was sent'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


