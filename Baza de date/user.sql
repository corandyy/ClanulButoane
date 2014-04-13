-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Gazda: localhost
-- Timp de generare: 13 Apr 2014 la 02:07
-- Versiune server: 5.5.24-log
-- Versiune PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Baza de date: `user`
--

-- --------------------------------------------------------

--
-- Structura de tabel pentru tabelul `products`
--

CREATE TABLE IF NOT EXISTS `products` (
  `id` int(4) NOT NULL AUTO_INCREMENT,
  `product` varchar(100) NOT NULL,
  `price` int(5) NOT NULL,
  `vp` int(10) NOT NULL,
  `category` varchar(100) NOT NULL,
  `nr_vote` bigint(100) NOT NULL,
  `sum_vote` bigint(100) NOT NULL,
  `avg_vote` int(2) NOT NULL DEFAULT '5',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=21 ;

--
-- Salvarea datelor din tabel `products`
--

INSERT INTO `products` (`id`, `product`, `price`, `vp`, `category`, `nr_vote`, `sum_vote`, `avg_vote`) VALUES
(1, 'Apples', 3, 5, 'Fruits', 1, 10, 10),
(2, 'Oranges', 4, 3, 'Fruits', 0, 0, 5),
(4, 'Grapes', 3, 5, 'Fruits', 3, 30, 10),
(5, 'Beer', 5, 3, 'Drinks', 0, 0, 5),
(6, 'Peaces', 0, 3, 'Fruits', 0, 0, 5),
(7, 'Tomatoes', 4, 5, 'Vegetables', 2, 20, 10),
(8, 'Chips', 10, 4, 'Snacks', 2, 14, 7),
(9, 'Da', 0, 3, 'Others', 0, 0, 5),
(10, 'Pork', 10, 5, 'Meat', 2, 20, 10),
(11, 'Bananans', 0, 3, 'Fruits', 0, 0, 5),
(12, 'Potatoes', 5, 5, 'Vegetables', 1, 10, 10),
(13, 'Seeds', 7, 4, 'Snacks', 5, 34, 7),
(14, 'Pinapples', 0, 3, 'Fruits', 0, 0, 5),
(15, 'Kiwis', 0, 5, 'Fruits', 2, 20, 10),
(16, 'Popcorn', 0, 5, 'Snacks', 1, 10, 10),
(17, 'Peanuts', 0, 3, 'Snacks', 0, 0, 5),
(18, 'Nuts', 0, 3, 'Snacks', 0, 0, 5),
(19, 'Fish', 0, 3, 'Meat', 0, 0, 5),
(20, 'Pizza', 0, 5, 'Others', 1, 10, 10);

-- --------------------------------------------------------

--
-- Structura de tabel pentru tabelul `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `username` varchar(25) NOT NULL,
  `parola` varchar(25) NOT NULL,
  `admin` tinyint(1) NOT NULL,
  `group` varchar(255) NOT NULL,
  `votepoints` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Salvarea datelor din tabel `user`
--

INSERT INTO `user` (`id`, `username`, `parola`, `admin`, `group`, `votepoints`) VALUES
(1, 'admin', 'admin', 1, 'individual', 55),
(5, 'Andrei', 'parola', 1, 'Grup2', 1),
(7, 'Mircea', 'da', 0, 'Grup2', 5),
(8, 'Cosmin', 'parola', 0, 'individual', 5);

-- --------------------------------------------------------

--
-- Structura de tabel pentru tabelul `wm`
--

CREATE TABLE IF NOT EXISTS `wm` (
  `message` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Salvarea datelor din tabel `wm`
--

INSERT INTO `wm` (`message`) VALUES
('General meeting at 8:00AM on Thursday , 12.05.2014');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
