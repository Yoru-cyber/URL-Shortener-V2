FROM node:alpine

WORKDIR /App

COPY . /App

RUN npm install -g @angular/cli

RUN npm install

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "5001"]