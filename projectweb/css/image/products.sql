-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Apr 21, 2024 at 11:56 PM
-- Server version: 10.3.39-MariaDB-0ubuntu0.20.04.2
-- PHP Version: 7.4.3-4ubuntu2.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sdavesql`
--

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `id` int(11) NOT NULL,
  `category` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `code` varchar(255) NOT NULL,
  `price` int(11) NOT NULL,
  `image_url` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`id`, `category`, `name`, `description`, `code`, `price`, `image_url`) VALUES
(16, 'content', 'soul', '+1000', 'k5tt6-mccd4-7789f-gtr2', 100, 'http://sdave.hu/uploads/66252d8bc3c8e_SoulImg.png'),
(17, 'dlc', 'Tech sword', 'Damage+', 't3k4d-l5h8w-ee412-zt67', 200, 'http://sdave.hu/uploads/66252e8339646_Screenshot 2024-04-21 165211.jpg'),
(18, 'content', 'soul', '+2000', 'k5tt6-mccd4-7689f-ggr2', 200, 'http://sdave.hu/uploads/66252ecf5f534_SoulImg.png'),
(19, 'content', 'heal potion', '+100', 'hrg5d-892pd-freen-231h', 1000, 'http://sdave.hu/uploads/66253be48d663_download (4).jfif'),
(20, 'content', 'heal', '+250', 'jk654-kdfr6-kor67-6942', 2000, 'http://sdave.hu/uploads/66253c38efc1d_download (7).jfif'),
(21, 'dlc', 'heal poti', '+500', '69699-kdfr6-kor67-69f2', 3000, 'http://sdave.hu/uploads/66253c7c23960_download (6).jfif'),
(22, 'content', 'invincibility', '1minute', '69696-96969-69696-9696', 5000, 'http://sdave.hu/uploads/66253d020d4b9_download (3).jfif'),
(23, 'dlc', 'invinc', '3 minute', 'hf420-hr671-fgt5d-bb98', 7000, 'http://sdave.hu/uploads/66253d646b177_download (2).jfif'),
(24, 'content', 'invincibilityty', '10minute', 'gortf-sta45-9934r-23sd', 30000, 'http://sdave.hu/uploads/66253ddc89703_download (5).jfif');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
