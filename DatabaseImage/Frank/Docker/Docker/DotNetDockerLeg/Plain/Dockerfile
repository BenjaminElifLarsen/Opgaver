FROM ubuntu:18.04

# install ubuntu updates
RUN apt-get update && apt-get upgrade -y 

# add some network tools
RUN apt-get install -y inetutils-ping iproute2

# add some editors and vc tools
RUN apt-get install -y git nano sudo

# add misc tools
RUN apt-get install -y wget curl wait-for-it 
