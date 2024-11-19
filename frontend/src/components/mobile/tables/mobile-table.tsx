import { Avatar, Button, Divider } from "antd"
import { Card, CardAddress, CardContainer, CardDescription, CardText, CardTextContainer } from "./style"
import { EllipsisOutlined } from "@ant-design/icons"

type MobileTableProps = {
    title: string;
    subTitle: string;
    description: string;
    image: string;
}

export const MobileTable: React.FC<MobileTableProps> = ({ title, subTitle, description, image }) => {
    return (
        <CardContainer>
            <Card>
                <Avatar size='small' src={<img src={image} loading='lazy' alt="avatar" />} />
                <CardTextContainer>
                    <CardText>{title}</CardText>
                    <CardAddress>{subTitle}</CardAddress>
                    <CardDescription>{description}</CardDescription>
                </CardTextContainer>
                <Button icon={<EllipsisOutlined />} type="text" style={{ marginLeft: "auto", width: "fit-content", height: "fit-content", padding: 0 }} />
            </Card>
        </CardContainer>
    )
}