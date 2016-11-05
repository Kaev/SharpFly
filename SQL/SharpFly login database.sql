/*
Navicat MySQL Data Transfer

Source Server         : local XAMPP
Source Server Version : 50516
Source Host           : localhost:3306
Source Database       : sharpfly_login

Target Server Type    : MYSQL
Target Server Version : 50516
File Encoding         : 65001

Date: 2016-11-05 13:33:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accounts`
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `accountname` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `banned` bit(1) NOT NULL,
  `verified` bit(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for `clusters`
-- ----------------------------
DROP TABLE IF EXISTS `clusters`;
CREATE TABLE `clusters` (
  `id` int(11) unsigned NOT NULL,
  `name` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
