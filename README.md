net core api with redis cache

# Usage

### Redis

start Redis server with docker

    docker run -d -p 6379:6379 -v /root/redis/data:/data -v /root/redis/redis.conf:/usr/local/etc/redis/redis.conf --name redis --restart=always redis:latest redis-server /usr/local/etc/redis/redis.conf

### web api

run && test function
