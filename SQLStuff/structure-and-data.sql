/*
 Navicat Premium Data Transfer

 Source Server         : Buh
 Source Server Type    : MySQL
 Source Server Version : 50719
 Source Host           : localhost:3306
 Source Schema         : buhdev

 Target Server Type    : MySQL
 Target Server Version : 50719
 File Encoding         : 65001

 Date: 04/11/2017 17:43:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Accounts
-- ----------------------------
DROP TABLE IF EXISTS `Accounts`;
CREATE TABLE `Accounts`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `lastname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `dob` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `email` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `password` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `salt` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `lastlogin` datetime(0) NULL DEFAULT NULL,
  `major` int(11) NULL DEFAULT NULL,
  `usertype` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `firstsemester` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_account__major`(`major`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 21 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Courses
-- ----------------------------
DROP TABLE IF EXISTS `Courses`;
CREATE TABLE `Courses`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `number` int(11) NULL DEFAULT NULL,
  `description` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `majorId` int(11) NOT NULL,
  `credithours` int(11) NOT NULL,
  `career` int(255) NULL DEFAULT NULL,
  `modeofinstruction` int(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_course__major`(`majorId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 17 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Courses
-- ----------------------------
INSERT INTO `courses` VALUES (1, 'Software Engineering', 442, 'Industry standard software development', 1, 4, 1, 1);
INSERT INTO `courses` VALUES (2, 'Signals and Systems', 205, 'Signals and Systems', 2, 4, 1, 1);
INSERT INTO `courses` VALUES (4, 'General Chemistry for Engineers', 108, 'General Chemistry for Engineers II', 4, 4, 1, 1);
INSERT INTO `courses` VALUES (5, 'General Chemistry for Engineers', 107, 'General Chemistry for Engineers I', 4, 4, 1, 1);
INSERT INTO `courses` VALUES (6, 'General Physics I', 107, 'General Physics I', 5, 4, 1, 1);
INSERT INTO `courses` VALUES (7, 'General Physics II', 108, 'General Physics II', 5, 4, 1, 1);
INSERT INTO `courses` VALUES (8, 'College Calculus 1', 141, 'College Calculus I', 3, 4, 1, 1);
INSERT INTO `courses` VALUES (9, 'College Calculus 2', 142, 'College Calculus II', 3, 4, 1, 1);
INSERT INTO `courses` VALUES (10, 'Introduction to Discrete Mathematics I', 191, 'Introduction to Discrete Mathematics I', 3, 4, 1, 1);
INSERT INTO `courses` VALUES (11, 'College Calculus 3', 241, 'College Calculus 3', 3, 4, 1, 1);
INSERT INTO `courses` VALUES (12, 'Introduction to Higher Mathematics', 311, 'Introduction to Higher Mathematics', 3, 4, 1, 1);
INSERT INTO `courses` VALUES (13, 'Intro to Probability I', 301, 'Intro to Probability I', 6, 4, 1, 1);
INSERT INTO `courses` VALUES (14, 'Intro to Probability II', 302, 'Intro to Probability II', 6, 4, 1, 1);
INSERT INTO `courses` VALUES (15, 'Introduction to Algorithms', 331, 'Intro to Algorithms', 1, 4, 1, 1);
INSERT INTO `courses` VALUES (16, 'Nanophotonics', 412, 'Nanophotonics', 2, 4, 1, 1);

-- ----------------------------
-- Table structure for Enrollments
-- ----------------------------
DROP TABLE IF EXISTS `Enrollments`;
CREATE TABLE `Enrollments`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountId` int(11) NOT NULL,
  `sectionId` int(11) NOT NULL,
  `status` int(11) NULL DEFAULT NULL,
  `dateadded` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_enrollment__account`(`accountId`) USING BTREE,
  INDEX `idx_enrollment__section`(`sectionId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 137 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;


-- ----------------------------
-- Table structure for Majors
-- ----------------------------
DROP TABLE IF EXISTS `Majors`;
CREATE TABLE `Majors`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `fullname` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `type` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'B.S.',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Majors
-- ----------------------------
INSERT INTO `majors` VALUES (1, 'CSE', 'Computer Science', 'B.S.');
INSERT INTO `majors` VALUES (2, 'EE', 'Electrical Engineering', 'B.S.');
INSERT INTO `majors` VALUES (3, 'MTH', 'Mathematics', 'B.S.');
INSERT INTO `majors` VALUES (4, 'CHE', 'Chemistry', 'B.A.');
INSERT INTO `majors` VALUES (5, 'PHY', 'Physics', 'B.S.');
INSERT INTO `majors` VALUES (6, 'STA', 'Statistics', 'B.S.');

-- ----------------------------
-- Table structure for notifications
-- ----------------------------
DROP TABLE IF EXISTS `notifications`;
CREATE TABLE `notifications`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `date` datetime(0) NULL DEFAULT NULL,
  `title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `content` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `accountId` int(11) NULL DEFAULT NULL,
  `status` int(255) NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Offerings
-- ----------------------------
DROP TABLE IF EXISTS `Offerings`;
CREATE TABLE `Offerings`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `semesterId` int(11) NOT NULL,
  `courseId` int(11) NOT NULL,
  `type` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `capacity` int(11) NULL DEFAULT NULL,
  `parentcourse` int(11) NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_offering__course`(`courseId`) USING BTREE,
  INDEX `idx_offering__semester`(`semesterId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 41 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Offerings
-- ----------------------------
INSERT INTO `offerings` VALUES (10, 1, 2, 'recitation', 10, 2);
INSERT INTO `offerings` VALUES (7, 1, 1, 'lecture', 10, 0);
INSERT INTO `offerings` VALUES (8, 1, 1, 'recitation', 10, 1);
INSERT INTO `offerings` VALUES (9, 1, 2, 'lecture', 10, 0);
INSERT INTO `offerings` VALUES (11, 1, 3, 'lecture', 50, 0);
INSERT INTO `offerings` VALUES (12, 1, 3, 'recitation', 30, 9);
INSERT INTO `offerings` VALUES (13, 1, 10, 'lecture', 50, 0);
INSERT INTO `offerings` VALUES (14, 1, 8, 'lecture', 140, 0);
INSERT INTO `offerings` VALUES (15, 1, 8, 'recitation', 50, 12);
INSERT INTO `offerings` VALUES (16, 1, 9, 'lecture', 150, 0);
INSERT INTO `offerings` VALUES (17, 1, 9, 'recitation', 50, 14);
INSERT INTO `offerings` VALUES (18, 1, 11, 'lecture', 150, 0);
INSERT INTO `offerings` VALUES (19, 1, 11, 'recitation', 100, 16);
INSERT INTO `offerings` VALUES (20, 1, 4, 'lecture', 100, 0);
INSERT INTO `offerings` VALUES (21, 1, 4, 'recitation', 40, 18);
INSERT INTO `offerings` VALUES (22, 1, 5, 'lecture', 200, 0);
INSERT INTO `offerings` VALUES (23, 1, 5, 'recitation', 50, 20);
INSERT INTO `offerings` VALUES (24, 1, 6, 'lecture', 155, 0);
INSERT INTO `offerings` VALUES (25, 1, 6, 'recitation', 50, 22);
INSERT INTO `offerings` VALUES (26, 2, 7, 'lecture', 100, 0);
INSERT INTO `offerings` VALUES (27, 2, 7, 'recitation', 100, 24);
INSERT INTO `offerings` VALUES (28, 1, 13, 'lecture', 80, 0);
INSERT INTO `offerings` VALUES (29, 1, 13, 'recitation', 80, 26);
INSERT INTO `offerings` VALUES (30, 2, 14, 'lecture', 80, 0);
INSERT INTO `offerings` VALUES (31, 2, 14, 'recitation', 80, 28);
INSERT INTO `offerings` VALUES (32, 1, 15, 'lecture', 200, 0);
INSERT INTO `offerings` VALUES (33, 1, 15, 'recitation', 50, 30);
INSERT INTO `offerings` VALUES (34, 1, 15, 'recitation', 50, 30);
INSERT INTO `offerings` VALUES (35, 1, 10, 'lecture', 90, 0);
INSERT INTO `offerings` VALUES (36, 1, 10, 'recitation', 80, 33);
INSERT INTO `offerings` VALUES (37, 1, 12, 'lecture', 50, 0);
INSERT INTO `offerings` VALUES (38, 1, 12, 'recitation', 50, 35);
INSERT INTO `offerings` VALUES (39, 1, 16, 'lecture', 40, 0);
INSERT INTO `offerings` VALUES (40, 1, 16, 'recitation', 40, 37);

-- ----------------------------
-- Table structure for Professors
-- ----------------------------
DROP TABLE IF EXISTS `Professors`;
CREATE TABLE `Professors`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `lastname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Professors
-- ----------------------------

INSERT INTO `professors` VALUES (1, 'Jimmy', 'Brooks');
INSERT INTO `professors` VALUES (2, 'Sandy', 'Robinson');
INSERT INTO `professors` VALUES (3, 'Melinda', 'Fink');
INSERT INTO `professors` VALUES (4, 'Jacob', 'Blake');

-- ----------------------------
-- Table structure for Sections
-- ----------------------------
DROP TABLE IF EXISTS `Sections`;
CREATE TABLE `Sections`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `offeringId` int(11) NOT NULL,
  `room` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `professorId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_section__offering`(`offeringId`) USING BTREE,
  INDEX `idx_section__professors`(`professorId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 39 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Sections
-- ----------------------------
INSERT INTO `sections` VALUES (1, 7, 'NSC 210', 1);
INSERT INTO `sections` VALUES (3, 8, 'NSC 216', 3);
INSERT INTO `sections` VALUES (4, 9, 'NSC 218', 4);
INSERT INTO `sections` VALUES (6, 10, 'NSE 202', 2);
INSERT INTO `sections` VALUES (9, 11, '211', 1);
INSERT INTO `sections` VALUES (10, 12, '200', 1);
INSERT INTO `sections` VALUES (11, 13, 'Math 210', 1);
INSERT INTO `sections` VALUES (12, 14, 'NSC 210', 1);
INSERT INTO `sections` VALUES (13, 15, 'NSC 211', 1);
INSERT INTO `sections` VALUES (14, 16, 'NSC 215', 3);
INSERT INTO `sections` VALUES (15, 17, 'NSC 216', 2);
INSERT INTO `sections` VALUES (16, 18, 'NSC 216', 2);
INSERT INTO `sections` VALUES (17, 19, 'NSC 210', 1);
INSERT INTO `sections` VALUES (18, 20, 'NSC 218', 1);
INSERT INTO `sections` VALUES (19, 21, 'C210', 1);
INSERT INTO `sections` VALUES (20, 22, 'C215', 1);
INSERT INTO `sections` VALUES (21, 23, 'C21', 1);
INSERT INTO `sections` VALUES (22, 24, 'NSC218', 1);
INSERT INTO `sections` VALUES (23, 25, 'C200', 3);
INSERT INTO `sections` VALUES (24, 26, 'NSC215', 2);
INSERT INTO `sections` VALUES (25, 27, 'C200', 2);
INSERT INTO `sections` VALUES (26, 28, 'C210', 4);
INSERT INTO `sections` VALUES (27, 29, 'C210', 4);
INSERT INTO `sections` VALUES (28, 30, 'C210', 4);
INSERT INTO `sections` VALUES (29, 31, 'C210', 1);
INSERT INTO `sections` VALUES (30, 32, 'K210', 3);
INSERT INTO `sections` VALUES (31, 33, 'K100', 1);
INSERT INTO `sections` VALUES (32, 34, 'K100', 1);
INSERT INTO `sections` VALUES (33, 35, 'K300', 2);
INSERT INTO `sections` VALUES (34, 36, 'K301', 1);
INSERT INTO `sections` VALUES (35, 37, 'M210', 2);
INSERT INTO `sections` VALUES (36, 38, 'M210', 1);
INSERT INTO `sections` VALUES (37, 39, 'N200', 4);
INSERT INTO `sections` VALUES (38, 40, 'N21', 1);

-- ----------------------------
-- Table structure for Semesters
-- ----------------------------
DROP TABLE IF EXISTS `Semesters`;
CREATE TABLE `Semesters`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `year` int(11) NULL DEFAULT NULL,
  `enrollopen` datetime(0) NULL DEFAULT NULL,
  `enrollclose` datetime(0) NULL DEFAULT NULL,
  `resignclose` datetime(0) NULL DEFAULT NULL,
  `startdate` datetime(0) NULL DEFAULT NULL,
  `enddate` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Semesters
-- ----------------------------
INSERT INTO `semesters` VALUES (1, 'Fall', 2017, '2017-08-01 21:21:52', '2017-12-25 21:22:01', '2017-11-30 21:22:07', '2017-08-28 21:22:13', '2017-12-23 21:22:19');
INSERT INTO `semesters` VALUES (2, 'Spring', 2017, '2016-11-20 21:27:42', '2017-01-31 21:27:53', '2017-04-14 21:28:04', '2017-01-30 21:28:12', '2017-05-19 21:28:18');

-- ----------------------------
-- Table structure for Timeslots
-- ----------------------------
DROP TABLE IF EXISTS `Timeslots`;
CREATE TABLE `Timeslots`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dayofweek` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `starttime` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `endtime` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `sectionId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_timeslot__section`(`sectionId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 64 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Timeslots
-- ----------------------------
INSERT INTO `timeslots` VALUES (1, 'Monday', '12:00', '13:00', 1);
INSERT INTO `timeslots` VALUES (2, 'Wednesday', '12:00', '13:00', 1);
INSERT INTO `timeslots` VALUES (3, 'Friday', '12:00', '13:00', 1);
INSERT INTO `timeslots` VALUES (4, 'Tuesday', '13:00', '14:00', 2);
INSERT INTO `timeslots` VALUES (5, 'Thursday', '13:00', '14:00', 2);
INSERT INTO `timeslots` VALUES (8, 'Friday', '08:00', '09:00', 3);
INSERT INTO `timeslots` VALUES (9, 'Tuesday', '13:00', '14:00', 4);
INSERT INTO `timeslots` VALUES (10, 'Thursday', '13:00', '14:00', 4);
INSERT INTO `timeslots` VALUES (11, 'Monday', '11:00', '12:00', 6);
INSERT INTO `timeslots` VALUES (13, 'Monday', '9:30', '10:30', 9);
INSERT INTO `timeslots` VALUES (14, 'Wednesday', '9:30', '10:30', 9);
INSERT INTO `timeslots` VALUES (15, 'Friday', '9:30', '10:30', 9);
INSERT INTO `timeslots` VALUES (16, 'Tuesday', '10:00', '10:50', 10);
INSERT INTO `timeslots` VALUES (17, 'Monday', '8:00', '8:50', 12);
INSERT INTO `timeslots` VALUES (18, 'Wednesday', '8:00', '8:50', 12);
INSERT INTO `timeslots` VALUES (19, 'Friday', '8:00', '8:50', 12);
INSERT INTO `timeslots` VALUES (20, 'Tuesday', '9:00', '9:50', 13);
INSERT INTO `timeslots` VALUES (21, 'Monday', '9:00', '9:50', 14);
INSERT INTO `timeslots` VALUES (22, 'Wednesday', '9:00', '9:50', 14);
INSERT INTO `timeslots` VALUES (23, 'Friday', '9:00', '9:50', 14);
INSERT INTO `timeslots` VALUES (24, 'Monday', '8:00', '8:50', 15);
INSERT INTO `timeslots` VALUES (25, 'Tuesday', '8:00', '9:20', 16);
INSERT INTO `timeslots` VALUES (26, 'Thursday', '8:00', '9:20', 16);
INSERT INTO `timeslots` VALUES (27, 'Thursday', '8:00', '8:50', 17);
INSERT INTO `timeslots` VALUES (28, 'Monday', '10:00', '10:50', 18);
INSERT INTO `timeslots` VALUES (29, 'Wednesday', '10:00', '10:50', 18);
INSERT INTO `timeslots` VALUES (30, 'Friday', '10:00', '10:50', 18);
INSERT INTO `timeslots` VALUES (31, 'Monday', '8:00', '8:50', 19);
INSERT INTO `timeslots` VALUES (32, 'Tuesday', '11:00', '12:20', 20);
INSERT INTO `timeslots` VALUES (33, 'Thursday', '11:00', '12:20', 20);
INSERT INTO `timeslots` VALUES (34, 'Saturday', '10:00', '10:50', 21);
INSERT INTO `timeslots` VALUES (35, 'Monday', '12:00', '12:50', 22);
INSERT INTO `timeslots` VALUES (36, 'Wednesday', '12:00', '12:50', 22);
INSERT INTO `timeslots` VALUES (37, 'Friday', '12:00', '12:50', 22);
INSERT INTO `timeslots` VALUES (38, 'Thursday', '10:00', '10:50', 23);
INSERT INTO `timeslots` VALUES (39, 'Monday', '8:00', '8:50', 24);
INSERT INTO `timeslots` VALUES (40, 'Wednesday', '8:00', '8:50', 24);
INSERT INTO `timeslots` VALUES (41, 'Friday', '8:00', '8:50', 24);
INSERT INTO `timeslots` VALUES (42, 'Friday', '9:00', '9:50', 25);
INSERT INTO `timeslots` VALUES (43, 'Tuesday', '6:00', '6:50', 26);
INSERT INTO `timeslots` VALUES (44, 'Thursday', '6:00', '6:50', 26);
INSERT INTO `timeslots` VALUES (45, 'Thursday', '7:00', '7:50', 27);
INSERT INTO `timeslots` VALUES (46, 'Tuesday', '6:00', '6:50', 28);
INSERT INTO `timeslots` VALUES (47, 'Thursday', '6:00', '6:50', 28);
INSERT INTO `timeslots` VALUES (48, 'Tuesday', '7:00', '7:50', 29);
INSERT INTO `timeslots` VALUES (49, 'Monday', '1:00', '1:50', 30);
INSERT INTO `timeslots` VALUES (50, 'Wednesday', '1:00', '1:50', 30);
INSERT INTO `timeslots` VALUES (51, 'Friday', '1:00', '1:50', 30);
INSERT INTO `timeslots` VALUES (52, 'Friday', '2:00', '2:50', 31);
INSERT INTO `timeslots` VALUES (53, 'Tuesday', '1:00', '1:50', 32);
INSERT INTO `timeslots` VALUES (54, 'Tuesday', '4:00', '5:20', 33);
INSERT INTO `timeslots` VALUES (55, 'Thursday', '4:00', '5:20', 33);
INSERT INTO `timeslots` VALUES (56, 'Tuesday', '6:00', '6:50', 34);
INSERT INTO `timeslots` VALUES (57, 'Monday', '12:00', '12:50', 35);
INSERT INTO `timeslots` VALUES (58, 'Wednesday', '12:00', '12:50', 35);
INSERT INTO `timeslots` VALUES (59, 'Friday', '12:00', '12:50', 35);
INSERT INTO `timeslots` VALUES (60, 'Monday', '8:00', '8:50', 36);
INSERT INTO `timeslots` VALUES (61, 'Tuesday', '16:00', '16:50', 37);
INSERT INTO `timeslots` VALUES (62, 'Thursday', '16:00', '16:50', 37);
INSERT INTO `timeslots` VALUES (63, 'Tuesday', '17:00', '17:50', 38);


SET FOREIGN_KEY_CHECKS = 1;
