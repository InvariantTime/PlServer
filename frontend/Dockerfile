FROM node:20-alpine AS build

WORKDIR /src

COPY ./package*.json ./

RUN ["npm", "install"]

COPY . .
RUN ["npm", "run", "build"]


FROM nginx:latest
WORKDIR /app

COPY --from=build src/build /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
