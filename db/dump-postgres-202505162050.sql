PGDMP      2                }            postgres    15.8    17.0 N    v           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            w           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            x           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            y           1262    5    postgres    DATABASE     t   CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.UTF-8';
    DROP DATABASE postgres;
                     postgres    false            z           0    0    DATABASE postgres    COMMENT     N   COMMENT ON DATABASE postgres IS 'default administrative connection database';
                        postgres    false    3705            {           0    0    DATABASE postgres    ACL     2   GRANT ALL ON DATABASE postgres TO dashboard_user;
                        postgres    false    3705            |           0    0    postgres    DATABASE PROPERTIES     >   ALTER DATABASE postgres SET "app.settings.jwt_exp" TO '3600';
                          postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                     pg_database_owner    false            }           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                        pg_database_owner    false    12            ~           0    0    SCHEMA public    ACL     �   GRANT USAGE ON SCHEMA public TO postgres;
GRANT USAGE ON SCHEMA public TO anon;
GRANT USAGE ON SCHEMA public TO authenticated;
GRANT USAGE ON SCHEMA public TO service_role;
                        pg_database_owner    false    12                       1259    30653    Branches    TABLE     [  CREATE TABLE public."Branches" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
    DROP TABLE public."Branches";
       public         heap r       postgres    false    12                       0    0    TABLE "Branches"    ACL     7  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Branches" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Branches" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Branches" TO service_role;
          public               postgres    false    276            	           1259    17264 
   Categories    TABLE     �  CREATE TABLE public."Categories" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "IconClass" text NOT NULL,
    "ParentId" uuid,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
     DROP TABLE public."Categories";
       public         heap r       postgres    false    12            �           0    0    TABLE "Categories"    ACL     =  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Categories" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Categories" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Categories" TO service_role;
          public               postgres    false    265                       1259    18490    CheckoutCounters    TABLE     c  CREATE TABLE public."CheckoutCounters" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
 &   DROP TABLE public."CheckoutCounters";
       public         heap r       postgres    false    12            �           0    0    TABLE "CheckoutCounters"    ACL     O  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."CheckoutCounters" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."CheckoutCounters" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."CheckoutCounters" TO service_role;
          public               postgres    false    275            
           1259    17276 	   Customers    TABLE     4  CREATE TABLE public."Customers" (
    "Id" uuid NOT NULL,
    "FirstName" text,
    "LastName" text DEFAULT ''::text NOT NULL,
    "NationalCode" text,
    "Mobile" text NOT NULL,
    "Email" text,
    "IsMobileVerified" boolean NOT NULL,
    "IsEmailVerified" boolean NOT NULL,
    "Password" text,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
    DROP TABLE public."Customers";
       public         heap r       postgres    false    12            �           0    0    TABLE "Customers"    ACL     :  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Customers" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Customers" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Customers" TO service_role;
          public               postgres    false    266                       1259    17345    ProductFeatures    TABLE     �  CREATE TABLE public."ProductFeatures" (
    "Id" uuid NOT NULL,
    "Key" text,
    "Value" text,
    "IsPinned" boolean NOT NULL,
    "Order" integer NOT NULL,
    "ProductId" uuid NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
 %   DROP TABLE public."ProductFeatures";
       public         heap r       postgres    false    12            �           0    0    TABLE "ProductFeatures"    ACL     L  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductFeatures" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductFeatures" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductFeatures" TO service_role;
          public               postgres    false    274                       1259    17283    ProductImages    TABLE     �  CREATE TABLE public."ProductImages" (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ImageUrl" text NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
 #   DROP TABLE public."ProductImages";
       public         heap r       postgres    false    12            �           0    0    TABLE "ProductImages"    ACL     F  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductImages" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductImages" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductImages" TO service_role;
          public               postgres    false    267                       1259    17290    ProductPriceLists    TABLE     �  CREATE TABLE public."ProductPriceLists" (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "Price" integer NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
 '   DROP TABLE public."ProductPriceLists";
       public         heap r       postgres    false    12            �           0    0    TABLE "ProductPriceLists"    ACL     R  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductPriceLists" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductPriceLists" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ProductPriceLists" TO service_role;
          public               postgres    false    268                       1259    17333    Products    TABLE     8  CREATE TABLE public."Products" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "ShortDescription" text NOT NULL,
    "FullDescription" text NOT NULL,
    "SKU" text NOT NULL,
    "DownloadUrl" text,
    "ProductType" smallint NOT NULL,
    "UnitId" uuid NOT NULL,
    "CategoryId" uuid NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
    DROP TABLE public."Products";
       public         heap r       postgres    false    12            �           0    0    TABLE "Products"    ACL     7  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Products" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Products" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Products" TO service_role;
          public               postgres    false    273                       1259    17321    ShippingAddresses    TABLE     �  CREATE TABLE public."ShippingAddresses" (
    "Id" uuid NOT NULL,
    "Country" text NOT NULL,
    "Province" text NOT NULL,
    "City" text NOT NULL,
    "Address" text NOT NULL,
    "PostalCode" text NOT NULL,
    "CustomerId" uuid NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
 '   DROP TABLE public."ShippingAddresses";
       public         heap r       postgres    false    12            �           0    0    TABLE "ShippingAddresses"    ACL     R  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ShippingAddresses" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ShippingAddresses" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."ShippingAddresses" TO service_role;
          public               postgres    false    272                       1259    17295    Stores    TABLE     �  CREATE TABLE public."Stores" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "PhoneNumber" text NOT NULL,
    "HostUrl" text NOT NULL,
    "Culture" text,
    "LogoUrl" text,
    "IsActive" boolean NOT NULL,
    "Version" integer NOT NULL,
    "Ordering" integer NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
    DROP TABLE public."Stores";
       public         heap r       postgres    false    12            �           0    0    TABLE "Stores"    ACL     1  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Stores" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Stores" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Stores" TO service_role;
          public               postgres    false    269                       1259    17302    Units    TABLE     �  CREATE TABLE public."Units" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "BaseUnitId" uuid,
    "Ratio" numeric NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL
);
    DROP TABLE public."Units";
       public         heap r       postgres    false    12            �           0    0    TABLE "Units"    ACL     .  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Units" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Units" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Units" TO service_role;
          public               postgres    false    270                       1259    17314    Users    TABLE     "  CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "UserName" text NOT NULL,
    "Mobile" text NOT NULL,
    "Password" text NOT NULL,
    "Ordering" integer NOT NULL,
    "Version" integer NOT NULL,
    "StoreId" uuid NOT NULL,
    "OwnerId" uuid NOT NULL,
    "InsertedBy" uuid NOT NULL,
    "UpdatedBy" uuid NOT NULL,
    "InsertDateTime" bigint NOT NULL,
    "UpdateDateTime" bigint NOT NULL,
    "Role" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);
    DROP TABLE public."Users";
       public         heap r       postgres    false    12            �           0    0    TABLE "Users"    ACL     .  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Users" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Users" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."Users" TO service_role;
          public               postgres    false    271                       1259    17259    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap r       postgres    false    12            �           0    0    TABLE "__EFMigrationsHistory"    ACL     ^  GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."__EFMigrationsHistory" TO anon;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."__EFMigrationsHistory" TO authenticated;
GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLE public."__EFMigrationsHistory" TO service_role;
          public               postgres    false    264            s          0    30653    Branches 
   TABLE DATA                 public               postgres    false    276   Mu       h          0    17264 
   Categories 
   TABLE DATA                 public               postgres    false    265   �v       r          0    18490    CheckoutCounters 
   TABLE DATA                 public               postgres    false    275   �w       i          0    17276 	   Customers 
   TABLE DATA                 public               postgres    false    266   �x       q          0    17345    ProductFeatures 
   TABLE DATA                 public               postgres    false    274   �z       j          0    17283    ProductImages 
   TABLE DATA                 public               postgres    false    267   �{       k          0    17290    ProductPriceLists 
   TABLE DATA                 public               postgres    false    268   �}       p          0    17333    Products 
   TABLE DATA                 public               postgres    false    273   /       o          0    17321    ShippingAddresses 
   TABLE DATA                 public               postgres    false    272   c�       l          0    17295    Stores 
   TABLE DATA                 public               postgres    false    269   }�       m          0    17302    Units 
   TABLE DATA                 public               postgres    false    270   �       n          0    17314    Users 
   TABLE DATA                 public               postgres    false    271   7�       g          0    17259    __EFMigrationsHistory 
   TABLE DATA                 public               postgres    false    264   ̈́       �           2606    30659    Branches PK_Branches 
   CONSTRAINT     X   ALTER TABLE ONLY public."Branches"
    ADD CONSTRAINT "PK_Branches" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Branches" DROP CONSTRAINT "PK_Branches";
       public                 postgres    false    276            �           2606    17270    Categories PK_Categories 
   CONSTRAINT     \   ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "PK_Categories" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."Categories" DROP CONSTRAINT "PK_Categories";
       public                 postgres    false    265            �           2606    18496 $   CheckoutCounters PK_CheckoutCounters 
   CONSTRAINT     h   ALTER TABLE ONLY public."CheckoutCounters"
    ADD CONSTRAINT "PK_CheckoutCounters" PRIMARY KEY ("Id");
 R   ALTER TABLE ONLY public."CheckoutCounters" DROP CONSTRAINT "PK_CheckoutCounters";
       public                 postgres    false    275            �           2606    17282    Customers PK_Customers 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "PK_Customers" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Customers" DROP CONSTRAINT "PK_Customers";
       public                 postgres    false    266            �           2606    17351 "   ProductFeatures PK_ProductFeatures 
   CONSTRAINT     f   ALTER TABLE ONLY public."ProductFeatures"
    ADD CONSTRAINT "PK_ProductFeatures" PRIMARY KEY ("Id");
 P   ALTER TABLE ONLY public."ProductFeatures" DROP CONSTRAINT "PK_ProductFeatures";
       public                 postgres    false    274            �           2606    17289    ProductImages PK_ProductImages 
   CONSTRAINT     b   ALTER TABLE ONLY public."ProductImages"
    ADD CONSTRAINT "PK_ProductImages" PRIMARY KEY ("Id");
 L   ALTER TABLE ONLY public."ProductImages" DROP CONSTRAINT "PK_ProductImages";
       public                 postgres    false    267            �           2606    17294 &   ProductPriceLists PK_ProductPriceLists 
   CONSTRAINT     j   ALTER TABLE ONLY public."ProductPriceLists"
    ADD CONSTRAINT "PK_ProductPriceLists" PRIMARY KEY ("Id");
 T   ALTER TABLE ONLY public."ProductPriceLists" DROP CONSTRAINT "PK_ProductPriceLists";
       public                 postgres    false    268            �           2606    17339    Products PK_Products 
   CONSTRAINT     X   ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "PK_Products" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Products" DROP CONSTRAINT "PK_Products";
       public                 postgres    false    273            �           2606    17327 &   ShippingAddresses PK_ShippingAddresses 
   CONSTRAINT     j   ALTER TABLE ONLY public."ShippingAddresses"
    ADD CONSTRAINT "PK_ShippingAddresses" PRIMARY KEY ("Id");
 T   ALTER TABLE ONLY public."ShippingAddresses" DROP CONSTRAINT "PK_ShippingAddresses";
       public                 postgres    false    272            �           2606    17301    Stores PK_Stores 
   CONSTRAINT     T   ALTER TABLE ONLY public."Stores"
    ADD CONSTRAINT "PK_Stores" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Stores" DROP CONSTRAINT "PK_Stores";
       public                 postgres    false    269            �           2606    17308    Units PK_Units 
   CONSTRAINT     R   ALTER TABLE ONLY public."Units"
    ADD CONSTRAINT "PK_Units" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Units" DROP CONSTRAINT "PK_Units";
       public                 postgres    false    270            �           2606    17320    Users PK_Users 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public                 postgres    false    271            �           2606    17263 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public                 postgres    false    264            �           1259    17357    IX_Categories_ParentId    INDEX     W   CREATE INDEX "IX_Categories_ParentId" ON public."Categories" USING btree ("ParentId");
 ,   DROP INDEX public."IX_Categories_ParentId";
       public                 postgres    false    265            �           1259    17358    IX_ProductFeatures_ProductId    INDEX     c   CREATE INDEX "IX_ProductFeatures_ProductId" ON public."ProductFeatures" USING btree ("ProductId");
 2   DROP INDEX public."IX_ProductFeatures_ProductId";
       public                 postgres    false    274            �           1259    17359    IX_Products_UnitId    INDEX     O   CREATE INDEX "IX_Products_UnitId" ON public."Products" USING btree ("UnitId");
 (   DROP INDEX public."IX_Products_UnitId";
       public                 postgres    false    273            �           1259    17360    IX_ShippingAddresses_CustomerId    INDEX     i   CREATE INDEX "IX_ShippingAddresses_CustomerId" ON public."ShippingAddresses" USING btree ("CustomerId");
 5   DROP INDEX public."IX_ShippingAddresses_CustomerId";
       public                 postgres    false    272            �           1259    17361    IX_Units_BaseUnitId    INDEX     Q   CREATE INDEX "IX_Units_BaseUnitId" ON public."Units" USING btree ("BaseUnitId");
 )   DROP INDEX public."IX_Units_BaseUnitId";
       public                 postgres    false    270            �           2606    17271 ,   Categories FK_Categories_Categories_ParentId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Categories"
    ADD CONSTRAINT "FK_Categories_Categories_ParentId" FOREIGN KEY ("ParentId") REFERENCES public."Categories"("Id");
 Z   ALTER TABLE ONLY public."Categories" DROP CONSTRAINT "FK_Categories_Categories_ParentId";
       public               postgres    false    265    265    3509            �           2606    17352 5   ProductFeatures FK_ProductFeatures_Products_ProductId    FK CONSTRAINT     �   ALTER TABLE ONLY public."ProductFeatures"
    ADD CONSTRAINT "FK_ProductFeatures_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES public."Products"("Id") ON DELETE CASCADE;
 c   ALTER TABLE ONLY public."ProductFeatures" DROP CONSTRAINT "FK_ProductFeatures_Products_ProductId";
       public               postgres    false    274    273    3528            �           2606    17340 !   Products FK_Products_Units_UnitId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "FK_Products_Units_UnitId" FOREIGN KEY ("UnitId") REFERENCES public."Units"("Id") ON DELETE CASCADE;
 O   ALTER TABLE ONLY public."Products" DROP CONSTRAINT "FK_Products_Units_UnitId";
       public               postgres    false    270    3520    273            �           2606    17328 ;   ShippingAddresses FK_ShippingAddresses_Customers_CustomerId    FK CONSTRAINT     �   ALTER TABLE ONLY public."ShippingAddresses"
    ADD CONSTRAINT "FK_ShippingAddresses_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES public."Customers"("Id") ON DELETE CASCADE;
 i   ALTER TABLE ONLY public."ShippingAddresses" DROP CONSTRAINT "FK_ShippingAddresses_Customers_CustomerId";
       public               postgres    false    272    3511    266            �           2606    17309    Units FK_Units_Units_BaseUnitId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Units"
    ADD CONSTRAINT "FK_Units_Units_BaseUnitId" FOREIGN KEY ("BaseUnitId") REFERENCES public."Units"("Id");
 M   ALTER TABLE ONLY public."Units" DROP CONSTRAINT "FK_Units_Units_BaseUnitId";
       public               postgres    false    270    3520    270            	           826    16484     DEFAULT PRIVILEGES FOR SEQUENCES    DEFAULT ACL     �  ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON SEQUENCES TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON SEQUENCES TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON SEQUENCES TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON SEQUENCES TO service_role;
          public               postgres    false    12            	           826    16485     DEFAULT PRIVILEGES FOR SEQUENCES    DEFAULT ACL     �  ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON SEQUENCES TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON SEQUENCES TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON SEQUENCES TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON SEQUENCES TO service_role;
          public               supabase_admin    false    12            	           826    16483     DEFAULT PRIVILEGES FOR FUNCTIONS    DEFAULT ACL     �  ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON FUNCTIONS TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON FUNCTIONS TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON FUNCTIONS TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON FUNCTIONS TO service_role;
          public               postgres    false    12            		           826    16487     DEFAULT PRIVILEGES FOR FUNCTIONS    DEFAULT ACL     �  ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON FUNCTIONS TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON FUNCTIONS TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON FUNCTIONS TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT ALL ON FUNCTIONS TO service_role;
          public               supabase_admin    false    12            	           826    16482    DEFAULT PRIVILEGES FOR TABLES    DEFAULT ACL     I  ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO service_role;
          public               postgres    false    12            	           826    16486    DEFAULT PRIVILEGES FOR TABLES    DEFAULT ACL     a  ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO postgres;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO anon;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO authenticated;
ALTER DEFAULT PRIVILEGES FOR ROLE supabase_admin IN SCHEMA public GRANT SELECT,INSERT,REFERENCES,DELETE,TRIGGER,TRUNCATE,UPDATE ON TABLES TO service_role;
          public               supabase_admin    false    12            s   7  x�ՓMJCA��=ţ�*42�ɼ��J���T��}f2��xw�õ��x��;}�EB ~$��rv����e������d|���|[�������l�Mİ �
�]RU�$����h.N&�n"��M�f�a�̏���v]�h���Jmz.��$pE(x�R�R��^��h8b�C}|:��k1@�	�7`����C�	پ}�|�n�	��Ln���8���3$�
�jP[rv��B�Z�z��R��k��5!�!q�`�MXmh_��}!���S�����@%$Q�X�R�و�!u�D�ld��d4�2c*      h   I  x����J1 �{�"x�B�d6�?<��P(l�}�P�����ċ�/��˘�z�Ђ �C �03|!��lt>���L\݅e��|�/W�M�9'��h&��]PU��@�-�X�DAfS9��@��׏��E���[��� �.�{���d���d �@`9���OtU�L91&J1CH9��LV'�s]�ro˾hɠ�VK_�q��qo��Z�{'j+P&N�@�\k�����Q�O��zI^;��"����e%��	�fY)�1���4čN�2�dAT��w�t�.�4aEU@�Tw���9'Ĭ�%���1���WJ�o�^��\>      r   �   x���v
Q���W((M��L�Sr�HM��/-q�/�+I-*VRs�	uV�P7M5HJL���MM23�5I4IѵL5O�M3M3���4�40LR�QP������=7�n���sk�������!P� 
t�P`2�� ��01%Q��0�D�$%9U7)%5I�85���4�255͈uD�k Tghnb􅉁���)�knlidhibdj�i��� (�Q�      i   �  x��Kj�A��>�G6n�*#��!�*%�q�y�硁Ҕ>����I�t�s��27�"�|[[�Ј���_�������0_��>]�w������׏���d�|��8=�M�9－{`S�� �H`�K	�ٰ�X,f�t�}�{�k}���������?2b���-]��n���ـ���#�ΰ�3��zL5ce�Zr�VWE�ј�'��֏�}�Ȱ�^����:l-a.@%�kDS =Vk����'�YR`B?=L�%z�a٘�'�:G��4���q�[��D��V:]3�-;n[�� ��6jU0��"��uD2&��qD3��ty���+��ΖPK6An%x�D�g#�Fj4�,=��C���V{Mz5I�}��!�e�B��8asT�q�?���;L��V��6_z��Q�`�����P(coSp'���,sM      q   H  x�吻JCA��<�b�����+V�D��̂PryR�B����їqsLial3�03?_�?�^I�?� �3?����8�3t��'�����; �mn��8�i�2$��@iR��!��^/��Tq�_�S�ȫ�GR��*��>��;�тh�$o@k-AP��ȡ�H9&W%�9ZPVh�-��m��PѨ���D>��
��2Z��w����i��R��P��>:i�vR��L��J9"����A!�FEi�y��6���X*A�ibƀ�X��%��T��Jr-R��Q\/�K�hSJ>���XM�b?b[�o�d      j   �  x�œ?o�1�����Hu;N��C��PA���qR!��~|�l=<DQ��~v�7Wn���������?����߿���y��w������ۏW7۫}����1)p��m����=���?��>j�q+���]s��jvԩӬ�����p屓��w�Ev���.��|�x�������眼�z$���>fĘy*K�>�St/��µdʽ6�H����p_KRk���+e�nF\�;���	j���HN4���4ͥyu�yӹX�ч�ZA���4%�z2_��|{��[�8�o�9-�<L:h�J�<��������+�Eч��4Wa�)P>�yi�Z3L9�H�,iDբ:^�ƷK�E�PR|2�b���Z��FRm��^�W�:��g���/�X:      k   �  x�Փ�j�1�{?ŏ�M�Fs�D�.�cb��H#�B ����l']��6!BH�i������嗻m}�y��ܾ�����ܟn}\�Ϸ�OW_/o�w;6*��"�A����q4�ڽ��Ŷ�n%�T�)M��u����T�[[���x���V�����x9�1,y8H
�>��h��%k�1&���{SS&I��V��?��ķ�S`J�����йk�>,����:z+�sV�E��q ��<)���hWsi�<�"���G�2gH+��1�#�UecA������E6i��"��kʍk��f��v��������B:U�P&/�)��a��U&R������sT���{6%�ð e!|5���y]��AP8���UY����˔�Q������'Ј6+      p   $  x�Ք�jU1��}�C7W�#�$�Ip��E�T��w�/E�\�EߣXТ�ŵ/qn�2���h�����9C2g2��2������`��;x6�;�o^�G��߿�G��������������Vrf���"8��.���l����t�Og_�ߗ���d<�~���+��lJ���N���nFW���%`��V"dK�w�=�Σ��Lsփp��m�Sp��ࢱe�i�۬\�Z��9��d���|-rmY���PSk��žF����]"M�<|��s;@B	�;4�Q�RP�Z����-0|W�,�_��x2��|�����*�D	c
�wǖ[�0�0Dc�^s2I�+�^Z��Hk�dPs0��4n�U9C�����g�]������׫��Ԋ�u�R��Nd����2	�**���#Iz��bɭO&q�]��`�g�b�f=�H\���dVr��!	�k�j��)2Ul&5$�m��l��d����Y��ut+�h�nUԂ�7W8[�2��jY�PM��/�#���J�@^�����Y��      o   
   x���          l   �   x���v
Q���W((M��L�S
.�/J-VRs�	uV�P7�],��(���''� ~�>>@�����������H6$��_\ben`jIK����/)*M�Q0�Q0��N��TVt�	�Դ��� E1>      m     x��нJ1���"\�
;�I&����
ޭ}�I�@DP�@������|s�iu�n%N1L1$����rq�bu��dw��fݟͻ�����]_4�b�N
#�JRW`x�@���Q+�d�2R+EQ�b������vMS2,���
��C��ɃF
}�����7�_��А�h��<�Ч�z��8��Ty �<� �w܅�'�_�}�z���_������7Ck��x�D��5X��%���gCg�4UrJ�~�b�9�MLA��&$)��f�oo��u      n   �  x�ݓ�N\1��}���H��=��&+!H�\j��#��K`W�>��	�m��e���.������i�}��'��N����Isu#�i��9[��r�9?8:;�4[��R�e&����.�8Ρ�N��M��~�{����z�]�O?��z�ٻ����^` V�������׋����iq�µj���ԑ3l=kC�h3�G� H�
`<�);��CH��O����ZD�@�6��e��'�"��֤���Y9������� ���Sn3�P
E$�+b�\���}��@0E��NEF��'c&�]����1���Χ�>/����i��wQ�l7���U.`��A-Ǡ��3����6�n8gkɐuue7��rX#t�Rۤ���%���EQ�d�y4z�      g   �   x��Ͻj�0���W!�8�`$ٲk:�ơ�ց��*�#Q����w�f��%���px׷��n8��x�j�VB��Ouq2(k��������}���S�)�e�b5%���E�Ei��H7�	�/I�S��n��V�`O�r�aE�����!��F\����f�����^��<7M�Mn��E��Y�2�W��)��ޜ4�~A��,��N�;����     