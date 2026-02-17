import sys 
from PyQt6.QtWidgets import QApplication
from myMainWindow import  MyMainWindow
from argparse import ArgumentParser

QApplication.setApplicationName("DeluxeEdit")
QApplication.setApplicationVersion("0.9.0")
parser = ArgumentParser(
                    prog='DeluxeEdit',
                    description='Advanced text editor',
                    epilog='Text at the bottom of help')
parser.add_argument('--hex', nargs='+', help='Whether we should do Hex View',required=False,default=False,dest="DoHexView")
parser.add_argument('path', nargs='?', help='bar help', required=False,default=None)
args = parser.parse_args()

parser.print_help()
#if __name__ == "__main__":
app = QApplication(sys.argv)
my = MyMainWindow()
argsLen=len(sys.argv)

if argsLen>1:
    autoloadPath= sys.argv[0]
    if argsLen>=2  and sys.argv in "--hex":
        AutoLoadHex=True
    my.autoLoadFile(autoloadPath,AutoLoadHex)

sys.exit(app.exec())
