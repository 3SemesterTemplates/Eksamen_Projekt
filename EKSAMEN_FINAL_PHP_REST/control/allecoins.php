<?php
/**
 * Created by PhpStorm.
 * User: Tas
 * Date: 08-01-2018
 * Time: 11:23
 */

//twig
require_once '../vendor/autoload.php';
Twig_Autoloader::register();


//$loader er en variable med navnet "loader"
$loader = new Twig_Loader_Filesystem('../view'); //fortæller hvor template er lokalisert
$twig = new Twig_Environment($loader, array(
    'auto_reload' => true
));




$template = $twig->loadTemplate('allecoins.html.twig'); //referer til allecoins.html.twig.twig så den kan bruges


$uri = "http://eksamenrest.azurewebsites.net/Service1.svc/coins";
$json = file_get_contents($uri);
$Liste = json_decode($json); //fortæller jeg gerne vil have det i json


$twigContent = array ("Coins" => $Liste); // laver et array med min variabel $liste
#print_r($twigContent);
echo $template->render($twigContent);
