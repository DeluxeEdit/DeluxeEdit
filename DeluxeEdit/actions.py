from urllib.parse import quote, unquote
class Actions:
  
  def run(self, indata,pluginId):return self.ecodeUrl(self,indata)
  def run(self, indata,pluginId):return self.decodeUrl(self,indata)
   
  @staticmethod
  def ecodeUrl(self,indata):
    result=quote(indata)
    return result
    
    @staticmethod
    def decodeUrl(self,indata):
        result=unquote(indata)
        return result