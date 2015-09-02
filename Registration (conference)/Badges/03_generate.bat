@echo off & perl -x %0 >04_generate.bat & pause & exit
#!perl
use strict;
use warnings;

# http://strawberryperl.com/ 
# http://www.imagemagick.org/
# export -> encode in utf-8 without bom

my $convert = qq|"C:\\Program Files\\ImageMagick-6.9.0-Q16\\convert.exe"|;
my $size = '1148x184';
my $size2 = '309x77';

my %churches;
open IN, '<', '02_export.csv' or die $!;
while(<IN>)
{
    chomp;
    my($id, $name, $church, $pv, $pv2, $sr, $so, $sv, $sv2, $nr, $no) = split /\s*;\s*/;
    my $idName2 = sprintf("%03d", $id);
    if($church eq '') { $church = ' '; }
    $churches{$church} = 1;
    open OUT, '>', "Names\\$idName2.txt" or die $!;
    print OUT $name;
    close OUT or die $!;
    print "$convert -background white -fill black -font Purista-Medium " .
        "-size $size -gravity center label:\@Names\\$idName2.txt Names\\$idName2.png\n";
    print "$convert -background black -transparent black -fill white -font Purista-Medium " .
        "-size $size2 -gravity center label:$idName2 Ids\\$idName2.png\n";
}

for my $name (keys %churches)
{
    my $name2 = $name;
    $name2 =~ s/([^A-Za-z])/sprintf('%2.2x', unpack('U0U*', $1))/ge;
    open OUT, '>', "Churches\\$name2.txt" or die $!;
    print OUT $name;
    close OUT or die $!;
    print "$convert -background white -fill black -font Purista-Medium " .
        "-size $size -gravity center label:\@Churches\\$name2.txt Churches\\$name2.png\n";
}


