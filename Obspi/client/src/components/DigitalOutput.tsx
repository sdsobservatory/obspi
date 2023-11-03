import { Stack, Switch, Typography } from "@mui/material";

interface DigitalOutputProps {
    name: string,
    value: boolean,
    handleChange: (name: string, value: boolean) => void,
}

function DigitalOutput(props: DigitalOutputProps) {
  return (
    <Stack direction="row" spacing={1} alignItems="center" key={props.name}>
      <Switch
        checked={props.value}
        name={props.name}
        onChange={(e) => props.handleChange(props.name, e.target.checked)} />
      <Typography>{props.name}</Typography>
    </Stack>
  )
}

export default DigitalOutput;