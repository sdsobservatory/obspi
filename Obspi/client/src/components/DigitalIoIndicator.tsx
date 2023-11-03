import { Circle, CircleOutlined } from "@mui/icons-material"
import { Checkbox, Stack, Typography } from "@mui/material"

interface DigitalIoIndicatorProps {
    label: string,
    value: boolean,
}

function DigitalIoIndicator(props: DigitalIoIndicatorProps) {
  return (
    <Stack direction="row" alignItems="center">
        <Checkbox
            disableRipple
            checked={props.value}
            icon={<CircleOutlined />}
            checkedIcon={<Circle />}
            sx={{ p: 0.85, cursor: 'default' }}
            readOnly={true}
        />
        <Typography>{props.label}</Typography>
    </Stack>
  )
}

export default DigitalIoIndicator