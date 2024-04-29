-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 29. 17:26
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `gamedata`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `personal`
--

CREATE TABLE `personal` (
  `id` int(11) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `birth_date` date NOT NULL,
  `home_address` varchar(255) NOT NULL,
  `bank_name` varchar(255) NOT NULL,
  `account_number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `personal`
--

INSERT INTO `personal` (`id`, `first_name`, `last_name`, `birth_date`, `home_address`, `bank_name`, `account_number`) VALUES
(1, 'admin', 'admin', '0000-00-00', 'Debrece, kerek utca 8/C', '', 0),
(2, 'kecske', 'kecske', '0000-00-00', 'Debrecen, Kecske utca 69', '', 0),
(3, 'here', 'here', '0000-00-00', 'Debrecen, Híd utca 34', '', 0),
(5, 'kerek', 'kerek', '2000-11-30', 'fgjdfkg', 'dfjsndfg', 3243235);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `products`
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
-- A tábla adatainak kiíratása `products`
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

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`id`, `username`, `email`, `password`) VALUES
(1, 'admin', 'admin@admin.com', '$2y$10$vXaLfQRM5O/f3EHIEMHwk.MdZ9gNtOTy3/.e74K1ra.hkazoKbZSO'),
(2, 'kecske', 'kecske@kecske.com', '$2y$10$G6RUA3r4m/cEOO0SkeDKGubTZNYy1v2NkwG1kB0b2Je.kY.Cuki/i'),
(3, 'here', 'here@here.com', '$2y$10$2nMvmF.ZfzdJTsAtHD2UneUwLgMvgoIp3iWtcwpplVKku4oX3jNEO'),
(4, 'fdfgdf', 'ddfgdf@jgffdgjdf.com', '$2y$10$iqPScfjG/AVqHSPXaEwe7OdsP.0qFdACk8sEUuzOamhxGgUudyjlG'),
(5, 'kerek', 'kerek@kerek.com', '$2y$10$CqmMAtvx8KFMnv6DvBmxv.pbNDqWlev2JDKQH.KwYPjvA63rOPy6m');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `personal`
--
ALTER TABLE `personal`
  ADD UNIQUE KEY `id` (`id`);

--
-- A tábla indexei `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `email_2` (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `products`
--
ALTER TABLE `products`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `personal`
--
ALTER TABLE `personal`
  ADD CONSTRAINT `personal_ibfk_1` FOREIGN KEY (`id`) REFERENCES `users` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
