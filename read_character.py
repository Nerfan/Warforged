#!/usr/bin/python3.5
"""
Create skeleton code for a character class.
Read a text file copy-pasted from the Warforged Doc.
"""

import sys
import re

def get_card(read, write, color):
    """
    Read a card's name and effect text and write to file.

    read should be at a point where the next non-empty line is a card name.

    Args:
        read (file open for reading): file to read card from
        write (file open for writing): file to write code to
    """
    name = read.readline().strip()
    while name == "":
        name = read.readline().strip()
    effect = read.readline().strip()[8:]
    temp = read.readline().strip()
    while temp != "":
        effect += "\\n" + temp
        temp = read.readline().strip()
    regex = re.compile('[^a-zA-Z]')
    classname = regex.sub("", name)
    write.write("\n\
        private class " + classname + " : Card\n\
        {\n\
            public " + classname + "(Character user) : base(user)\n\
            {\n\
                name = \"" + name + "\";\n\
                effect = \"" + effect + "\";\n\
                color = " + color + ";\n\
            }\n\
\n\
            public override void activate()\n\
            {\n\
                //TODO\n\
            }\n\
        }\n\
")



def get_character(read):
    """
    Read the character's name and bolster and write to file.

    Write the header for the file as well.
    read should be at the beginning of the file,
    and the first line should be NAME, TITLE

    Args:
        read (file open for reading): file to read character from

    Returns:
        A file open for writing (named after the character)
    """
    line = read.readline().split(",")
    name = line[0]
    title = line[1].strip()
    write = open("Warforged/Characters/"+name+".cs", "w")
    write.write("\
using System;\n\
using System.Windows.Media;\n\
using System.Windows.Media.Imaging;\n\
\n\
namespace Warforged\n\
{\n\
    public class " + name + " : Character\n\
    {\n\
        public " + name + "() : base()\n\
        {\n\
            name = \"" + name + "\";\n\
            title = \"" + title + "\";\n\
        }\n\
")
    return write

def main(filename):
    """
    Read a file and write the skeleton code for a character
    """
    read = open(filename)
    write = get_character(read)
    color = "Color.red"
    while read.readline().strip() != "Offense (Red)":
        pass
    last_pos = read.tell()
    line = read.readline()
    while line != "":
        if line.strip() == "Offense (Red)":
            color = "Color.red"
        elif line.strip() == "Intent (Green)":
            color = "Color.green"
        elif line.strip() == "Defense (Blue)":
            color = "Color.blue"
        elif line.strip() == "Inherent Cards":
            color = "Color.black"
        elif line.strip() == "Awakening Cards":
            color = "Color.black; //TODO"
        elif line.strip() == "":
            pass
        else:
            read.seek(last_pos)
            get_card(read, write, color)
        last_pos = read.tell()
        line = read.readline()
    write.write("\
    }\n\
}")


if __name__ == "__main__":
    sys.argv
    if len(sys.argv) != 2:
        print("Usage: python3.5 read_character.py filename")
    else:
        main(sys.argv[1])
