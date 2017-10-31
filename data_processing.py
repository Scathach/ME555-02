"""
import xlwt
from temfile import TemporaryFile
book = xlwt.Workbook()
sheet = book.add_sheet('sheet1')

sum_of_targets_at_time = []
for r in range(len(targets_observed)):
    sum_of_targets_at_time.append(sum(targets_observed[r])/number_of_robots)

for i,e in enumerate(sum_of_targets_at_time):
    sheet.write(i,l,i)
    sheet.write(i,0,e)

name = "data.xls"
book.save(name)
"""
#time vs average distance of each robot from the centroid of distribution of robots

#and

#time vs average distance of the centroid from the closes point on the line through the course

robots = []
number_of_collisions = []
distance_to_horizontal = []
distance_to_vertical = []
distance_to_target = []
r = []
d = open("data.txt","r")
r.append(d.readlines()[1].split(":")[0])
d.close()

data = open("data.txt","r")
number_of_robots = 0

for line in data:
    name = line.split(":")[0]
    if name == r[0] and number_of_robots !=0:
        break
    elif name != 'New Run\n':
        robots.append(name)
        number_of_robots += 1


horizontal_distance = []
vertical_distance = []
time = []

counter = 0
horizontal_dis = 0
vertical_dis = 0
t = 0 
for line in data:
    if len(line.split(":")) != 1:
        if counter < 25:
            counter += 1 
            horizontal_dis += float(line.split(":")[2])
            vertical_dis += float(line.split(":")[3])
            t = float((line.split(":")[5])[:-1])
            
        else:

            horizontal_distance.append(horizontal_dis/counter)
            vertical_distance.append(vertical_dis/counter)
            time.append(t)
            counter = 0
            horizontal_dis = 0
            vertical_dis = 0
            t = 0 

print(time)
            #print(robots)


