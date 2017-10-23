/*
 Navicat Premium Data Transfer

 Source Server         : Local Dev
 Source Server Type    : MySQL
 Source Server Version : 100122
 Source Host           : localhost:3306
 Source Schema         : buh

 Target Server Type    : MySQL
 Target Server Version : 100122
 File Encoding         : 65001

 Date: 22/10/2017 13:28:05
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
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
) ENGINE = MyISAM AUTO_INCREMENT = 9 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for courses
-- ----------------------------
DROP TABLE IF EXISTS `courses`;
CREATE TABLE `courses`  (
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
-- Records of courses
-- ----------------------------
INSERT INTO `courses` VALUES (1, 'Software Engineering', 442, 'Just resign now.', 1, 4, 1, 1);
INSERT INTO `courses` VALUES (2, 'Circuit Design', 205, 'Yeah!', 2, 4, 1, 1);

-- ----------------------------
-- Table structure for enrollments
-- ----------------------------
DROP TABLE IF EXISTS `enrollments`;
CREATE TABLE `enrollments`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountId` int(11) NOT NULL,
  `sectionId` int(11) NOT NULL,
  `status` int(11) NULL DEFAULT NULL,
  `dateadded` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_enrollment__account`(`accountId`) USING BTREE,
  INDEX `idx_enrollment__section`(`sectionId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 58 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of enrollments
-- ----------------------------
INSERT INTO `enrollments` VALUES (56, 8, 1, 1, '2017-10-21 18:05:36');
INSERT INTO `enrollments` VALUES (57, 8, 3, 1, '2017-10-21 18:05:36');

-- ----------------------------
-- Table structure for majors
-- ----------------------------
DROP TABLE IF EXISTS `majors`;
CREATE TABLE `majors`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `fullname` varchar(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `type` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL DEFAULT 'B.S.',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of majors
-- ----------------------------
INSERT INTO `majors` VALUES (1, 'CSE', 'Computer Science', 'B.S.');
INSERT INTO `majors` VALUES (2, 'EE', 'Electrical Engineering', 'B.S.');

-- ----------------------------
-- Table structure for offerings
-- ----------------------------
DROP TABLE IF EXISTS `offerings`;
CREATE TABLE `offerings`  (
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
-- Records of offerings
-- ----------------------------
INSERT INTO `offerings` VALUES (10, 1, 2, 'recitation', 10, 2);
INSERT INTO `offerings` VALUES (7, 1, 1, 'lecture', 10, 0);
INSERT INTO `offerings` VALUES (8, 1, 1, 'recitation', 10, 1);
INSERT INTO `offerings` VALUES (9, 1, 2, 'lecture', 10, 0);

-- ----------------------------
-- Table structure for professors
-- ----------------------------
DROP TABLE IF EXISTS `professors`;
CREATE TABLE `professors`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `lastname` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of professors
-- ----------------------------
INSERT INTO `professors` VALUES (1, 'Prof', 'One');
INSERT INTO `professors` VALUES (2, 'Poff', 'Two');
INSERT INTO `professors` VALUES (3, 'Prof', 'Three');
INSERT INTO `professors` VALUES (4, 'Prof', 'Four\r\n');

-- ----------------------------
-- Table structure for sections
-- ----------------------------
DROP TABLE IF EXISTS `sections`;
CREATE TABLE `sections`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `offeringId` int(11) NOT NULL,
  `room` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `professorId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_section__offering`(`offeringId`) USING BTREE,
  INDEX `idx_section__professors`(`professorId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 9 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sections
-- ----------------------------
INSERT INTO `sections` VALUES (1, 7, 'NSC 210', 1);
INSERT INTO `sections` VALUES (3, 8, 'NSC 216', 3);
INSERT INTO `sections` VALUES (4, 9, 'NSC 218', 4);
INSERT INTO `sections` VALUES (6, 10, 'NSE 202', 2);

-- ----------------------------
-- Table structure for semesters
-- ----------------------------
DROP TABLE IF EXISTS `semesters`;
CREATE TABLE `semesters`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `season` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `year` int(11) NULL DEFAULT NULL,
  `enrollopen` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `enrollclose` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `resignclose` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `startdate` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `enddate` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of semesters
-- ----------------------------
INSERT INTO `semesters` VALUES (1, 'Fall', 2017, '2017-08-01 21:21:52', '2017-10-31 21:22:01', '2017-11-30 21:22:07', '2017-08-28 21:22:13', '2017-12-23 21:22:19');
INSERT INTO `semesters` VALUES (2, 'Spring', 2018, '2017-11-20 21:27:42', '2018-02-06 21:27:53', '2018-04-13 21:28:04', '2018-01-29 21:28:12', '2018-05-18 21:28:18');

-- ----------------------------
-- Table structure for timeslots
-- ----------------------------
DROP TABLE IF EXISTS `timeslots`;
CREATE TABLE `timeslots`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dayofweek` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `starttime` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `endtime` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
  `sectionId` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_timeslot__section`(`sectionId`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 13 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of timeslots
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

SET FOREIGN_KEY_CHECKS = 1;
