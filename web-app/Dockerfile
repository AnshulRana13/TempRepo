### STAGE 1: Build ###
FROM node:latest as build
WORKDIR /app
COPY /web-app/package.json /web-app/package-lock.json ./
RUN npm install
# Add the source code to app
COPY ./web-app ./
RUN npm run build
#EXPOSE 4200

### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
# Copy the build output to replace the default nginx contents.

COPY --from=build /app/dist/web-app /usr/share/nginx/html
#COPY --from=build-stage /default.conf /etc/nginx/conf.d/default.conf