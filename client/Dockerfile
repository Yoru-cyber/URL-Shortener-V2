# Use a lightweight base image
FROM node:alpine 

# Set working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN yarn install 

# Copy the rest of the application code
COPY . .

# Build the application for production (adjust build command if needed)
CMD ["yarn", "dev"] 