@echo off & perl -x %0 & pause & exit
#!perl
use strict;
use warnings;

open IN, '<', 'zbory.txt' or die $!;
open SQL, '>', 'zbory.sql' or die $!;
open CS, '>', 'zbory.cs' or die $!;

print SQL "DELETE FROM [dbo].[zbor];\nGO\n";
print CS "ddlZbor.Items.Add(new ListItem(\"Prosím vyberte zbor\", \"0\"));\n";
my %h;
my $i = 1;
while(<IN>)
{
    chomp;
    print SQL "INSERT INTO [dbo].[zbor] ([id], [name]) VALUES ($i, N'$_');\n";
    $h{$_} = $i++;
}
for(sort keys %h)
{
    print CS "ddlZbor.Items.Add(new ListItem(\"$_\", \"$h{$_}\"));\n";
}
print SQL "GO\n";
print CS "ddlZbor.Items.Add(new ListItem(\"Iný...\", \"-1\"));\n";
