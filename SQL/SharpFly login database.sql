SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accounts`
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Accountname` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Banned` bit(1) NOT NULL,
  `Verified` bit(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;