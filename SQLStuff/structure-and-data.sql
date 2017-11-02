/*
 Navicat Premium Data Transfer

 Source Server         : Buh
 Source Server Type    : MySQL
 Source Server Version : 50637
 Source Host           : albatross.jm-corp.net:3306
 Source Schema         : buhavore_buh

 Target Server Type    : MySQL
 Target Server Version : 50637
 File Encoding         : 65001

 Date: 01/11/2017 19:52:14
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
-- Records of Accounts
-- ----------------------------


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
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Courses
-- ----------------------------
INSERT INTO `Courses` VALUES (1, 'Software Engineering', 442, 'Just resign now.', 1, 4, 1, 1);
INSERT INTO `Courses` VALUES (2, 'Circuit Design', 205, 'Yeah!', 2, 4, 1, 1);

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
) ENGINE = MyISAM AUTO_INCREMENT = 96 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of Enrollments
-- ----------------------------


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
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Majors
-- ----------------------------
INSERT INTO `Majors` VALUES (1, 'CSE', 'Computer Science', 'B.S.');
INSERT INTO `Majors` VALUES (2, 'EE', 'Electrical Engineering', 'B.S.');

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
) ENGINE = MyISAM AUTO_INCREMENT = 11 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Offerings
-- ----------------------------
INSERT INTO `Offerings` VALUES (10, 1, 2, 'recitation', 10, 2);
INSERT INTO `Offerings` VALUES (7, 1, 1, 'lecture', 10, 0);
INSERT INTO `Offerings` VALUES (8, 1, 1, 'recitation', 10, 1);
INSERT INTO `Offerings` VALUES (9, 1, 2, 'lecture', 10, 0);

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
INSERT INTO `Professors` VALUES (1, 'Prof', 'One');
INSERT INTO `Professors` VALUES (2, 'Poff', 'Two');
INSERT INTO `Professors` VALUES (3, 'Prof', 'Three');
INSERT INTO `Professors` VALUES (4, 'Prof', 'Four\r\n');

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
) ENGINE = MyISAM AUTO_INCREMENT = 9 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Sections
-- ----------------------------
INSERT INTO `Sections` VALUES (1, 7, 'NSC 210', 1);
INSERT INTO `Sections` VALUES (3, 8, 'NSC 216', 3);
INSERT INTO `Sections` VALUES (4, 9, 'NSC 218', 4);
INSERT INTO `Sections` VALUES (6, 10, 'NSE 202', 2);

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
INSERT INTO `Semesters` VALUES (1, 'Fall', 2017, '2017-11-01 19:38:56', '2017-12-25 21:22:01', '2017-12-23 19:38:56', '2017-08-27 19:38:56', '2017-12-29 19:38:56');
INSERT INTO `Semesters` VALUES (2, 'Spring', 2018, '2017-11-20 21:27:42', '2018-02-06 21:27:53', '2018-04-13 21:28:04', '2018-01-29 21:28:12', '2018-05-18 21:28:18');

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
) ENGINE = MyISAM AUTO_INCREMENT = 13 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Timeslots
-- ----------------------------
INSERT INTO `Timeslots` VALUES (1, 'Monday', '12:00', '13:00', 1);
INSERT INTO `Timeslots` VALUES (2, 'Wednesday', '12:00', '13:00', 1);
INSERT INTO `Timeslots` VALUES (3, 'Friday', '12:00', '13:00', 1);
INSERT INTO `Timeslots` VALUES (4, 'Tuesday', '13:00', '14:00', 2);
INSERT INTO `Timeslots` VALUES (5, 'Thursday', '13:00', '14:00', 2);
INSERT INTO `Timeslots` VALUES (8, 'Friday', '08:00', '09:00', 3);
INSERT INTO `Timeslots` VALUES (9, 'Tuesday', '13:00', '14:00', 4);
INSERT INTO `Timeslots` VALUES (10, 'Thursday', '13:00', '14:00', 4);
INSERT INTO `Timeslots` VALUES (11, 'Monday', '11:00', '12:00', 6);

SET FOREIGN_KEY_CHECKS = 1;
