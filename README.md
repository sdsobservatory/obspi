# Obspi

Observatory command and control API and dashboard for Sonoran Desert Skies Observatory (SDSO).

We use the dashboard every clear night.

![Dashboard](/assets/dashboard.png?raw=true)
	
# Features

  * Roof control and status
  * Automatic roof close at dawn
  * SMS notifications when roof is opened or closed, automatic roof close failure
  * Power cycle telescope pier power
  * Manual IO control
  * Multiple camera streams
  * Live allsky camera stream
  * Median guide error chart
  * Embedded grafana widgets showing temperature, humidity, etc.
  * Embedded GOES imagery
  * Raspi diagnostics

# Hardware

This application runs in a container on a Raspberry Pi 4 4gb.
The raspi has HATs from [Sequent Microsystems](https://sequentmicrosystems.com/)

  * industrial automation controller
  * 2x 16-channel output relays
  * 1x 16-channel input

# Architecture

ASP.NET Core backend with a React frontend dashboard.


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