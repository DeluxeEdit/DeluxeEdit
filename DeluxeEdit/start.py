import sys 
from PyQt6.QtWidgets import QApplication
from myMainWindow import  MyMainWindow
from argparse import ArgumentParser



my = MyMainWindow()
QApplication.setApplicationName("DeluxeEdit")
QApplication.setApplicationVersion("0.9.0")
parser = ArgumentParser(
                    prog='DeluxeEdit',
                    description='Advanced text editor',
                    epilog='Text at the bottom of help')

parser.add_argument('--hex', nargs='+', help='Whether we should do Hex View',required=False,default=False,dest='DoHexView')
parser.add_argument('--help',required=False,dest='DoHelp')
parser.add_argument('path', nargs='?', help='Whanted path', required=False,default=None)
parsed = parser.parse_args()

if parsed.path:
    my.autoLoadFile(parsed.path,   parsed.DoHexView)

if parsed.DoHelp:   parser.print_help()


app = QApplication(sys.argv)
sys.exit(app.exec())
