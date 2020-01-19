CREATE DATABASE IF NOT EXISTS logistics;
GRANT ALL PRIVILEGES ON logistics.* TO 'api'@'%' IDENTIFIED BY 'api';
GRANT ALL PRIVILEGES ON logistics.* TO 'api'@'localhost' IDENTIFIED BY 'api';
USE logistics;

CREATE TABLE IF NOT EXISTS `tb_member` (
  `member_id` int(10) unsigned NOT NULL,
  `forename` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `phone_number` varchar(13) NOT NULL,
  `address_line_1` varchar(255) DEFAULT NULL,
  `address_line_2` varchar(255) DEFAULT NULL,
  `address_line_3` varchar(255) DEFAULT NULL,
  `postcode` varchar(8) DEFAULT NULL,
  `is_partner` tinyint(4) DEFAULT 0,
  `create_date` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  `modified_date` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  `delete_date` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  PRIMARY KEY (`member_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `tb_auth` (
  `member_id` int(10) unsigned NOT NULL,
  `password` varchar(255) NOT NULL,
  `last_login` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  PRIMARY KEY (`member_id`),
  CONSTRAINT `tb_auth_ibfk_1` FOREIGN KEY (`member_id`) REFERENCES `tb_member` (`member_id`) ON DELETE CASCASE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `tb_order` (
  `order_id` int(10) unsigned NOT NULL,
  `recipient_id` int(10) unsigned NOT NULL,
  `sender` varchar(255) NOT NULL,
  `delivery_state` varchar(10) NOT NULL,
  `delivery_start_date` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  `delivery_end_date` int(10) unsigned DEFAULT NULL COMMENT 'unixtime',
  PRIMARY KEY (`order_id`),
  CONSTRAINT `tb_order_ibfk_1` FOREIGN KEY (`recipient_id`) REFERENCES `tb_member` (`member_id`) ON DELETE CASCASE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;