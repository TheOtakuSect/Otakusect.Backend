Insert into UserRoles(Id,Role) values
('84606cae-594d-472b-a1f4-5a91878d0de9','SectMaster'),
('b2d85906-2bb2-41e7-8d5e-85800a4d5f4e','SectElder'),
('f5f8eda2-be15-48dc-b5e5-51008897fc34','Disciple');

-- PASSWORD FOR ADMIN: ImmortalMaster --
Insert into Users(Id,EmailAddress,FullName,UserName,Password,UserRoleId,CreatedDateTime) values
('f12cd3e8-6ddf-4cf1-8278-85645701e87e','sectmaster@gmail.com','Sect Master','TheSectMaster','d4080174076d5cccd30f3d8935ddaf8b516c510e49542dd8fd4de4f4a6556b1f','84606cae-594d-472b-a1f4-5a91878d0de9',GETDATE());
