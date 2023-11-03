# OBSPI

Observatory control for SDSO.

This application runs in a container on a Raspberry Pi 4 4gb.
The raspi has HATs from [Sequent Microsystems](https://sequentmicrosystems.com/)

  * industrial automation controller
  * 2x 16-channel output relays
  * 1x 16-channel input

# Build

```shell
docker buildx build --push --pull -t registry.local.sdso.space/obspi -f Obspi/Dockerfile --platform linux/arm64 .
```

# Run

```shell
docker compose up -d
```

or to run temporarily

```shell
docker run -d --restart=unless-stopped --pull=always -p 8080:80 --name obspi --privileged registry.local.sdso.space/obspi
```