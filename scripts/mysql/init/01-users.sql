TRUNCATE TABLE `logistics`.`tb_auth`;
TRUNCATE TABLE `logistics`.`tb_member`;

INSERT INTO `logistics`.`tb_member` (
    `forename`,
    `surname`,
    `email`,
    `phone_number`,
    `address_line_1`,
    `address_line_2`,
    `address_line_3`,
    `postcode`,
    `is_partner`
) VALUES (
    'Denis',
    'Craig',
    'user@gluehome.com',
    '07123456789',
    '123 Fake Street',
    '2nd Avenue',
    'Landan',
    'EC5 4EZ',
    0
);

INSERT INTO `logistics`.`tb_auth` (
    `member_id`,
    `password`
) VALUES (
    1,
    'test'
);

INSERT INTO `logistics`.`tb_member` (
    `forename`,
    `surname`,
    `email`,
    `phone_number`,
    `address_line_1`,
    `address_line_2`,
    `address_line_3`,
    `postcode`,
    `is_partner`
) VALUES (
    'Danis',
    'Creig',
    'partner@gluehome.com',
    '07123456789',
    '123 Faker Street',
    '3rd Avenue',
    'Landan',
    'EC4 5EZ',
    1
);

INSERT INTO `logistics`.`tb_auth` (
    `member_id`,
    `password`
) VALUES (
    2,
    'test'
);