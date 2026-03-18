import sys 
from PyQt6.QtWidgets import QApplication
from myMainWindow import  MyMainWindow
from argparse import ArgumentParser
import argparse
app = QApplication(sys.argv)


my = MyMainWindow()
QApplication.setApplicationName("DeluxeEdit")
QApplication.setApplicationVersion("0.9.0")
parser = ArgumentParser(
                    prog='DeluxeEdit',
                    description='Advanced Text Editor',
                    epilog='Text at the bottom of help')

parser.add_argument('--hex', nargs='+', help='Whether we should do Hex View',default=False,dest='DoHexView')
parser.add_argument('path', nargs='?', help='Whanted path', default=None)
parsed = parser.parse_args()

if parsed.path:
    my.autoLoadFile(parsed.path,   parsed.DoHexView)



sys.exit(app.exec())
