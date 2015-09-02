@echo off & perl -x %0 & pause & exit
#!perl
use strict;
use warnings;

open IN, '<', 'tricka.txt' or die $!;
open SQL, '>', 'tricka.sql' or die $!;
open CS, '>', 'tricka.cs' or die $!;

print SQL "DELETE FROM [dbo].[tricko];\nGO\n";

print CS "ddlTricko.Items.Add(new ListItem(\"Žiadne\", \"0\"));\n";

my $i = 1;
while(<IN>)
{
    chomp;
    print SQL "INSERT INTO [dbo].[tricko] ([id], [name]) VALUES ($i, N'$_');\n";
    print CS "ddlTricko.Items.Add(new ListItem(\"$_\", \"$i\"));\n";
    $i++;
}
print SQL "GO\n";
