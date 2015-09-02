@echo off & perl -x %0 >06_merge.bat & pause & exit
#!perl
use strict;
use warnings;

# http://strawberryperl.com/ 
# http://www.imagemagick.org/
# Image::Magick 
my $convert = qq|"C:\\Program Files\\ImageMagick-6.9.0-Q16\\convert.exe"|;
my $x_name = 299;
my $y_name = 535;
my $x_church = 305;
my $y_church = 868;
my $x_check = 640;
my $x_id = 1232;
my $y_id = 727;
my $x_id2 = 724;
my $y_id2 = 1088;

my %churches;
open IN, '<', '02_export.csv' or die $!;
while(<IN>)
{
    chomp;
    my($id, $name, $church, $pv, $pv2, $sr, $so, $sv, $sv2, $nr, $no) = split /\s*;\s*/;
    my $idName2 = sprintf("%03d", $id);
    if($church eq '') { $church = ' '; }
    my $churchName2 = $church;
    $churchName2 =~ s/([^A-Za-z])/sprintf('%2.2x', unpack('U0U*', $1))/ge;
    if(1)
    {
        print "$convert v03_stA_300.png " .
            "Names\\$idName2.png -geometry +$x_name+$y_name -composite " .
            "Churches\\$churchName2.png -geometry +$x_church+$y_church -composite " .
            "Ids\\$idName2.png -geometry +$x_id+$y_id -composite " .
            "Result\\${idName2}_front.png\n";
    }
    if(1)
    {
        print "$convert v06_stB_300.jpg " .
            ($pv  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+220 -composite " .
            ($pv2 ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+308 -composite " .
            ($sr  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+501 -composite " .
            ($so  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+634 -composite " .
            ($sv  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+767 -composite " .
            ($sv2 ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+855 -composite " .
            ($nr  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+1045 -composite " .
            ($no  ? "check_yes.png" : "check_no.png") . " -geometry +$x_check+1133 -composite " .
            "Ids\\$idName2.png -geometry +$x_id2+$y_id2 -composite " .
            "Result\\${idName2}_back.png\n";
    }
}
