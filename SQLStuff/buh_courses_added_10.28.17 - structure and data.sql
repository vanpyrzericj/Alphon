-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.19-log - MySQL Community Server (GPL)
-- Server OS:                    Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for buh
CREATE DATABASE IF NOT EXISTS `buh` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `buh`;

-- Dumping structure for table buh.accounts
CREATE TABLE IF NOT EXISTS `accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) DEFAULT NULL,
  `lastname` varchar(255) DEFAULT NULL,
  `dob` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `salt` varchar(255) DEFAULT NULL,
  `lastlogin` datetime DEFAULT NULL,
  `major` int(11) DEFAULT NULL,
  `usertype` varchar(255) DEFAULT NULL,
  `firstsemester` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_account__major` (`major`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.accounts: 2 rows
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` (`id`, `firstname`, `lastname`, `dob`, `email`, `password`, `salt`, `lastlogin`, `major`, `usertype`, `firstsemester`) VALUES
	(15, 'test', 'buh', NULL, 'test@buh', '$2a$06$4m1Yk0dXJNJM3wbEG5RI/.mPYa9mOuQvzBuCPs004.0LQHZFKT2l.', NULL, '2017-10-28 12:47:17', NULL, 'Student', 1),
	(14, 'buh', 'admin', NULL, 'admin@buh', '$2a$06$zjBxR43HfLuDORCCilrppe8tMWK8037qJQO/W4u6luuALOS/CFUIG', NULL, '2017-10-28 12:47:26', NULL, 'Admin', 1);
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;

-- Dumping structure for table buh.courses
CREATE TABLE IF NOT EXISTS `courses` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `number` int(11) DEFAULT NULL,
  `description` varchar(255) NOT NULL,
  `majorId` int(11) NOT NULL,
  `credithours` int(11) NOT NULL,
  `career` int(255) DEFAULT NULL,
  `modeofinstruction` int(255) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_course__major` (`majorId`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=17 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.courses: 15 rows
/*!40000 ALTER TABLE `courses` DISABLE KEYS */;
INSERT INTO `courses` (`id`, `name`, `number`, `description`, `majorId`, `credithours`, `career`, `modeofinstruction`) VALUES
	(1, 'Software Engineering', 442, 'Industry standard software development', 1, 4, 1, 1),
	(2, 'Signals and Systems', 205, 'Signals and Systems', 2, 4, 1, 1),
	(4, 'General Chemistry for Engineers', 108, 'General Chemistry for Engineers II', 4, 4, 1, 1),
	(5, 'General Chemistry for Engineers', 107, 'General Chemistry for Engineers I', 4, 4, 1, 1),
	(6, 'General Physics I', 107, 'General Physics I', 5, 4, 1, 1),
	(7, 'General Physics II', 108, 'General Physics II', 5, 4, 1, 1),
	(8, 'College Calculus 1', 141, 'College Calculus I', 3, 4, 1, 1),
	(9, 'College Calculus 2', 142, 'College Calculus II', 3, 4, 1, 1),
	(10, 'Introduction to Discrete Mathematics I', 191, 'Introduction to Discrete Mathematics I', 3, 4, 1, 1),
	(11, 'College Calculus 3', 241, 'College Calculus 3', 3, 4, 1, 1),
	(12, 'Introduction to Higher Mathematics', 311, 'Introduction to Higher Mathematics', 3, 4, 1, 1),
	(13, 'Intro to Probability I', 301, 'Intro to Probability I', 6, 4, 1, 1),
	(14, 'Intro to Probability II', 302, 'Intro to Probability II', 6, 4, 1, 1),
	(15, 'Introduction to Algorithms', 331, 'Intro to Algorithms', 1, 4, 1, 1),
	(16, 'Nanophotonics', 412, 'Nanophotonics', 2, 4, 1, 1);
/*!40000 ALTER TABLE `courses` ENABLE KEYS */;

-- Dumping structure for table buh.enrollments
CREATE TABLE IF NOT EXISTS `enrollments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountId` int(11) NOT NULL,
  `sectionId` int(11) NOT NULL,
  `status` int(11) DEFAULT NULL,
  `dateadded` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_enrollment__account` (`accountId`) USING BTREE,
  KEY `idx_enrollment__section` (`sectionId`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=97 DEFAULT CHARSET=latin1 ROW_FORMAT=FIXED;

-- Dumping data for table buh.enrollments: 7 rows
/*!40000 ALTER TABLE `enrollments` DISABLE KEYS */;
INSERT INTO `enrollments` (`id`, `accountId`, `sectionId`, `status`, `dateadded`) VALUES
	(56, 8, 1, 1, '2017-10-21 18:05:36'),
	(89, 9, 1, 2, '2017-10-22 22:03:42'),
	(84, 11, 3, 2, '2017-10-22 21:51:27'),
	(83, 11, 1, 2, '2017-10-22 21:51:27'),
	(87, 12, 4, 1, '2017-10-22 22:00:36'),
	(88, 12, 6, 1, '2017-10-22 22:00:36'),
	(96, 13, 10, 1, '2017-10-28 11:58:17');
/*!40000 ALTER TABLE `enrollments` ENABLE KEYS */;

-- Dumping structure for table buh.majors
CREATE TABLE IF NOT EXISTS `majors` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `fullname` varchar(100) NOT NULL,
  `type` varchar(255) NOT NULL DEFAULT 'B.S.',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.majors: 6 rows
/*!40000 ALTER TABLE `majors` DISABLE KEYS */;
INSERT INTO `majors` (`id`, `name`, `fullname`, `type`) VALUES
	(1, 'CSE', 'Computer Science', 'B.S.'),
	(2, 'EE', 'Electrical Engineering', 'B.S.'),
	(3, 'MTH', 'Mathematics', 'B.S.'),
	(4, 'CHE', 'Chemistry', 'B.A.'),
	(5, 'PHY', 'Physics', 'B.S.'),
	(6, 'STA', 'Statistics', 'B.S.');
/*!40000 ALTER TABLE `majors` ENABLE KEYS */;

-- Dumping structure for table buh.offerings
CREATE TABLE IF NOT EXISTS `offerings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `semesterId` int(11) NOT NULL,
  `courseId` int(11) NOT NULL,
  `type` varchar(255) NOT NULL,
  `capacity` int(11) DEFAULT NULL,
  `parentcourse` int(11) DEFAULT '0',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_offering__course` (`courseId`) USING BTREE,
  KEY `idx_offering__semester` (`semesterId`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=41 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.offerings: 34 rows
/*!40000 ALTER TABLE `offerings` DISABLE KEYS */;
INSERT INTO `offerings` (`id`, `semesterId`, `courseId`, `type`, `capacity`, `parentcourse`) VALUES
	(10, 1, 2, 'recitation', 10, 2),
	(7, 1, 1, 'lecture', 10, 0),
	(8, 1, 1, 'recitation', 10, 1),
	(9, 1, 2, 'lecture', 10, 0),
	(11, 1, 3, 'lecture', 50, 0),
	(12, 1, 3, 'recitation', 30, 9),
	(13, 1, 10, 'lecture', 50, 0),
	(14, 1, 8, 'lecture', 140, 0),
	(15, 1, 8, 'recitation', 50, 12),
	(16, 1, 9, 'lecture', 150, 0),
	(17, 1, 9, 'recitation', 50, 14),
	(18, 1, 11, 'lecture', 150, 0),
	(19, 1, 11, 'recitation', 100, 16),
	(20, 1, 4, 'lecture', 100, 0),
	(21, 1, 4, 'recitation', 40, 18),
	(22, 1, 5, 'lecture', 200, 0),
	(23, 1, 5, 'recitation', 50, 20),
	(24, 1, 6, 'lecture', 155, 0),
	(25, 1, 6, 'recitation', 50, 22),
	(26, 2, 7, 'lecture', 100, 0),
	(27, 1, 7, 'recitation', 100, 24),
	(28, 1, 13, 'lecture', 80, 0),
	(29, 1, 13, 'recitation', 80, 26),
	(30, 2, 14, 'lecture', 80, 0),
	(31, 1, 14, 'recitation', 80, 28),
	(32, 1, 15, 'lecture', 200, 0),
	(33, 1, 15, 'recitation', 50, 30),
	(34, 1, 15, 'recitation', 50, 30),
	(35, 1, 10, 'lecture', 90, 0),
	(36, 1, 10, 'recitation', 80, 33),
	(37, 1, 12, 'lecture', 50, 0),
	(38, 1, 12, 'recitation', 50, 35),
	(39, 1, 16, 'lecture', 40, 0),
	(40, 1, 16, 'recitation', 40, 37);
/*!40000 ALTER TABLE `offerings` ENABLE KEYS */;

-- Dumping structure for table buh.professors
CREATE TABLE IF NOT EXISTS `professors` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) NOT NULL,
  `lastname` varchar(255) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.professors: 4 rows
/*!40000 ALTER TABLE `professors` DISABLE KEYS */;
INSERT INTO `professors` (`id`, `firstname`, `lastname`) VALUES
	(1, 'Prof', 'One'),
	(2, 'Poff', 'Two'),
	(3, 'Prof', 'Three'),
	(4, 'Prof', 'Four\r\n');
/*!40000 ALTER TABLE `professors` ENABLE KEYS */;

-- Dumping structure for table buh.sections
CREATE TABLE IF NOT EXISTS `sections` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `offeringId` int(11) NOT NULL,
  `room` varchar(255) NOT NULL,
  `professorId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_section__offering` (`offeringId`) USING BTREE,
  KEY `idx_section__professors` (`professorId`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=39 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.sections: 34 rows
/*!40000 ALTER TABLE `sections` DISABLE KEYS */;
INSERT INTO `sections` (`id`, `offeringId`, `room`, `professorId`) VALUES
	(1, 7, 'NSC 210', 1),
	(3, 8, 'NSC 216', 3),
	(4, 9, 'NSC 218', 4),
	(6, 10, 'NSE 202', 2),
	(9, 11, '211', 1),
	(10, 12, '200', 1),
	(11, 13, 'Math 210', 1),
	(12, 14, 'NSC 210', 1),
	(13, 15, 'NSC 211', 1),
	(14, 16, 'NSC 215', 3),
	(15, 17, 'NSC 216', 2),
	(16, 18, 'NSC 216', 2),
	(17, 19, 'NSC 210', 1),
	(18, 20, 'NSC 218', 1),
	(19, 21, 'C210', 1),
	(20, 22, 'C215', 1),
	(21, 23, 'C21', 1),
	(22, 24, 'NSC218', 1),
	(23, 25, 'C200', 3),
	(24, 26, 'NSC215', 2),
	(25, 27, 'C200', 2),
	(26, 28, 'C210', 4),
	(27, 29, 'C210', 4),
	(28, 30, 'C210', 4),
	(29, 31, 'C210', 1),
	(30, 32, 'K210', 3),
	(31, 33, 'K100', 1),
	(32, 34, 'K100', 1),
	(33, 35, 'K300', 2),
	(34, 36, 'K301', 1),
	(35, 37, 'M210', 2),
	(36, 38, 'M210', 1),
	(37, 39, 'N200', 4),
	(38, 40, 'N21', 1);
/*!40000 ALTER TABLE `sections` ENABLE KEYS */;

-- Dumping structure for table buh.semesters
CREATE TABLE IF NOT EXISTS `semesters` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` varchar(255) NOT NULL,
  `year` int(11) DEFAULT NULL,
  `enrollopen` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `enrollclose` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `resignclose` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `startdate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `enddate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.semesters: 2 rows
/*!40000 ALTER TABLE `semesters` DISABLE KEYS */;
INSERT INTO `semesters` (`id`, `season`, `year`, `enrollopen`, `enrollclose`, `resignclose`, `startdate`, `enddate`) VALUES
	(1, 'Fall', 2017, '2017-08-01 21:21:52', '2017-10-31 21:22:01', '2017-11-30 21:22:07', '2017-08-28 21:22:13', '2017-12-23 21:22:19'),
	(2, 'Spring', 2018, '2017-11-20 21:27:42', '2018-02-06 21:27:53', '2018-04-13 21:28:04', '2018-01-29 21:28:12', '2018-05-18 21:28:18');
/*!40000 ALTER TABLE `semesters` ENABLE KEYS */;

-- Dumping structure for table buh.timeslots
CREATE TABLE IF NOT EXISTS `timeslots` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dayofweek` varchar(255) NOT NULL,
  `starttime` varchar(255) NOT NULL,
  `endtime` varchar(255) NOT NULL,
  `sectionId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `idx_timeslot__section` (`sectionId`) USING BTREE
) ENGINE=MyISAM AUTO_INCREMENT=64 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table buh.timeslots: 60 rows
/*!40000 ALTER TABLE `timeslots` DISABLE KEYS */;
INSERT INTO `timeslots` (`id`, `dayofweek`, `starttime`, `endtime`, `sectionId`) VALUES
	(1, 'Monday', '12:00', '13:00', 1),
	(2, 'Wednesday', '12:00', '13:00', 1),
	(3, 'Friday', '12:00', '13:00', 1),
	(4, 'Tuesday', '13:00', '14:00', 2),
	(5, 'Thursday', '13:00', '14:00', 2),
	(8, 'Friday', '08:00', '09:00', 3),
	(9, 'Tuesday', '13:00', '14:00', 4),
	(10, 'Thursday', '13:00', '14:00', 4),
	(11, 'Monday', '11:00', '12:00', 6),
	(13, 'Monday', '9:30', '10:30', 9),
	(14, 'Wednesday', '9:30', '10:30', 9),
	(15, 'Friday', '9:30', '10:30', 9),
	(16, 'Tuesday', '10:00', '10:50', 10),
	(17, 'Monday', '8:00', '8:50', 12),
	(18, 'Wednesday', '8:00', '8:50', 12),
	(19, 'Friday', '8:00', '8:50', 12),
	(20, 'Tuesday', '9:00', '9:50', 13),
	(21, 'Monday', '9:00', '9:50', 14),
	(22, 'Wednesday', '9:00', '9:50', 14),
	(23, 'Friday', '9:00', '9:50', 14),
	(24, 'Monday', '8:00', '8:50', 15),
	(25, 'Tuesday', '8:00', '9:20', 16),
	(26, 'Thursday', '8:00', '9:20', 16),
	(27, 'Thursday', '8:00', '8:50', 17),
	(28, 'Monday', '10:00', '10:50', 18),
	(29, 'Wednesday', '10:00', '10:50', 18),
	(30, 'Friday', '10:00', '10:50', 18),
	(31, 'Monday', '8:00', '8:50', 19),
	(32, 'Tuesday', '11:00', '12:20', 20),
	(33, 'Thursday', '11:00', '12:20', 20),
	(34, 'Saturday', '10:00', '10:50', 21),
	(35, 'Monday', '12:00', '12:50', 22),
	(36, 'Wednesday', '12:00', '12:50', 22),
	(37, 'Friday', '12:00', '12:50', 22),
	(38, 'Thursday', '10:00', '10:50', 23),
	(39, 'Monday', '8:00', '8:50', 24),
	(40, 'Wednesday', '8:00', '8:50', 24),
	(41, 'Friday', '8:00', '8:50', 24),
	(42, 'Friday', '9:00', '9:50', 25),
	(43, 'Tuesday', '6:00', '6:50', 26),
	(44, 'Thursday', '6:00', '6:50', 26),
	(45, 'Thursday', '7:00', '7:50', 27),
	(46, 'Tuesday', '6:00', '6:50', 28),
	(47, 'Thursday', '6:00', '6:50', 28),
	(48, 'Tuesday', '7:00', '7:50', 29),
	(49, 'Monday', '1:00', '1:50', 30),
	(50, 'Wednesday', '1:00', '1:50', 30),
	(51, 'Friday', '1:00', '1:50', 30),
	(52, 'Friday', '2:00', '2:50', 31),
	(53, 'Tuesday', '1:00', '1:50', 32),
	(54, 'Tuesday', '4:00', '5:20', 33),
	(55, 'Thursday', '4:00', '5:20', 33),
	(56, 'Tuesday', '6:00', '6:50', 34),
	(57, 'Monday', '12:00', '12:50', 35),
	(58, 'Wednesday', '12:00', '12:50', 35),
	(59, 'Friday', '12:00', '12:50', 35),
	(60, 'Monday', '8:00', '8:50', 36),
	(61, 'Tuesday', '16:00', '16:50', 37),
	(62, 'Thursday', '16:00', '16:50', 37),
	(63, 'Tuesday', '17:00', '17:50', 38);
/*!40000 ALTER TABLE `timeslots` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
