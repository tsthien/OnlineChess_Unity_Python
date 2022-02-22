import socket
from _thread import *
import json
import numpy as np
from array_handler import array_to_json
import threading

class ChessServer:
    def __init__(self, localIP = "127.0.0.1", localPort = 20001, bufferSize = 1024):
        self.localIP     = localIP
        self.localPort   = localPort
        self.bufferSize  = bufferSize
        self.clients_list = []
        self.UDPServerSocket = None
        self.t1_row1 = np.array([3, 4, 5, 1, 2, 5, 4, 3])
        self.t1_row2 = np.array([6, 6, 6, 6, 6, 6, 6, 6])
        self.t1_row3 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
        self.t1_row4 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
        self.t1_row5 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
        self.t1_row6 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
        self.t1_row7 = np.array([12, 12, 12, 12, 12, 12, 12, 12])
        self.t1_row8 = np.array([9, 10, 11, 8, 7, 11, 10, 9])
        self.turn = 1
        self.win = 0
        
        self.rev_win_1 = 0
        self.rev_win_2 = 0
        
        self.count_ready = 0

    def __del__(self):
        self.UDPServerSocket.close()
        print("Close")
        
    def is_win(self):
        king1 = 0
        king7 = 0
        for val in self.t1_row1:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row2:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row3:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row4:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row5:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row6:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row7:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        for val in self.t1_row8:
            if val == 1:
                king1 += 1
            if val == 7:
                king7 += 1
        if king1 == 0:
            return 2
        if king7 == 0:
            return 1
        return 0
        
    
    def mapping_reverse(self, t1_row1, t1_row2, t1_row3, t1_row4, t1_row5, t1_row6, t1_row7, t1_row8, 
                t2_row1, t2_row2, t2_row3, t2_row4, t2_row5, t2_row6, t2_row7, t2_row8):
        mapping = [0, 7, 8, 9, 10, 11, 12, 1, 2, 3, 4, 5, 6]
        i = 0
        for val in t1_row1:
            t2_row1[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row2:
            t2_row2[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row3:
            t2_row3[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row4:
            t2_row4[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row5:
            t2_row5[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row6:
            t2_row6[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row7:
            t2_row7[i] = mapping[val]
            i += 1
        i = 0
        for val in t1_row8:
            t2_row8[i] = mapping[val]
            i += 1
            
        t2_row1 = np.array(t2_row1)
        t2_row2 = np.array(t2_row2)
        t2_row3 = np.array(t2_row3)
        t2_row4 = np.array(t2_row4)
        t2_row5 = np.array(t2_row5)
        t2_row6 = np.array(t2_row6)
        t2_row7 = np.array(t2_row7)
        t2_row8 = np.array(t2_row8)

    def endcode_msg(self, msg):
        return str.encode(msg)

    def run_4ever(self):
        # Create a datagram socket
        self.UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
        # Bind to address and ip
        self.UDPServerSocket.bind((self.localIP, self.localPort))

        print("UDP server up and listening")
        # Listen for incoming datagrams

        while(True):

            bytesAddressPair = self.UDPServerSocket.recvfrom(self.bufferSize)

            message = bytesAddressPair[0]

            address = bytesAddressPair[1]

            if address not in self.clients_list:
                self.clients_list.append(address)
            try:
                js = json.loads(message.decode())
                #print(js)
            except:
                pass
            if js['ping'] == 0 and js['start'] == 0 and js['row1'] == [] and js['row2'] == [] and js['row3'] == [] and js['row4'] == [] and js['row5'] == [] and js['row6'] == [] and js['row7'] == [] and js['row8'] == []:
                if self.clients_list.index(address) == 0:
                    self.rev_win_1 = 1
                    self.UDPServerSocket.sendto(self.endcode_msg("999"), address)
                if self.clients_list.index(address) == 1:
                    self.rev_win_2 = 1
                    self.UDPServerSocket.sendto(self.endcode_msg("999"), address)
            if self.rev_win_1 == 1 and self.rev_win_2 == 1:
                #reset
                self.rev_win_1 = 0
                self.rev_win_2 = 0
                self.win = 0
                self.turn = 1
                self.clients_list = []
                self.count_ready = 0
                self.t1_row1 = np.array([3, 4, 5, 1, 2, 5, 4, 3])
                self.t1_row2 = np.array([6, 6, 6, 6, 6, 6, 6, 6])
                self.t1_row3 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
                self.t1_row4 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
                self.t1_row5 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
                self.t1_row6 = np.array([0, 0, 0, 0, 0, 0, 0, 0])
                self.t1_row7 = np.array([12, 12, 12, 12, 12, 12, 12, 12])
                self.t1_row8 = np.array([9, 10, 11, 8, 7, 11, 10, 9])
            if js['start'] == 1:
                self.count_ready += 1
                self.UDPServerSocket.sendto(self.endcode_msg("YouReady!"), address)
                print(self.count_ready)
            elif js['ping'] == 0 and js['start'] == 0 and js['row1'] != [] and js['row2'] != [] and js['row3'] != [] and js['row4'] != [] and js['row5'] != [] and js['row6'] != [] and js['row7'] != [] and js['row8'] != []:
                #Them kiem tra win
                if self.clients_list.index(address) == 0:
                    self.turn = 2
                    self.t1_row1 = np.array(js['row1'])
                    self.t1_row2 = np.array(js['row2'])
                    self.t1_row3 = np.array(js['row3'])
                    self.t1_row4 = np.array(js['row4'])
                    self.t1_row5 = np.array(js['row5'])
                    self.t1_row6 = np.array(js['row6'])
                    self.t1_row7 = np.array(js['row7'])
                    self.t1_row8 = np.array(js['row8'])
                    self.UDPServerSocket.sendto(self.endcode_msg("Updated!"), address)
                elif self.clients_list.index(address) == 1:
                    self.turn = 1
                    self.t1_row1 = np.array(js['row1'])
                    self.t1_row2 = np.array(js['row2'])
                    self.t1_row3 = np.array(js['row3'])
                    self.t1_row4 = np.array(js['row4'])
                    self.t1_row5 = np.array(js['row5'])
                    self.t1_row6 = np.array(js['row6'])
                    self.t1_row7 = np.array(js['row7'])
                    self.t1_row8 = np.array(js['row8'])
                    self.UDPServerSocket.sendto(self.endcode_msg("Updated!"), address)
                self.win = self.is_win()
            elif js['ping'] == 1 and self.count_ready == 2:
                if self.win == 0:
                    if self.clients_list.index(address) == 0:
                        self.UDPServerSocket.sendto(self.endcode_msg(array_to_json(self.turn, 0, 1, self.t1_row1, self.t1_row2, self.t1_row3
                                                            , self.t1_row4, self.t1_row5, self.t1_row6, self.t1_row7, self.t1_row8)), address)
                    if self.clients_list.index(address) == 1:
                        self.UDPServerSocket.sendto(self.endcode_msg(array_to_json(self.turn, 0, 2, self.t1_row1, self.t1_row2, self.t1_row3
                                                            , self.t1_row4, self.t1_row5, self.t1_row6, self.t1_row7, self.t1_row8)), address)
                elif self.win != 0:
                    if self.clients_list.index(address) == 0:
                        self.UDPServerSocket.sendto(self.endcode_msg(array_to_json(0, self.win, 1, self.t1_row1, self.t1_row2, self.t1_row3
                                                                , self.t1_row4, self.t1_row5, self.t1_row6, self.t1_row7, self.t1_row8)), address)
                    if self.clients_list.index(address) == 1:
                        self.UDPServerSocket.sendto(self.endcode_msg(array_to_json(0, self.win, 2, self.t1_row1, self.t1_row2, self.t1_row3
                                                                , self.t1_row4, self.t1_row5, self.t1_row6, self.t1_row7, self.t1_row8)), address)
            elif js['ping'] == 1 and self.count_ready != 2:
                self.UDPServerSocket.sendto(self.endcode_msg("People not ready!"), address)
                    
            
if __name__ == '__main__':
    cs = ChessServer()
    cs.run_4ever()
