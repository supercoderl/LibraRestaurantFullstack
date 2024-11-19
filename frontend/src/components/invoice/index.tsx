import { Container, Section, Title, LogoContainer, InfoContainer, InvoiceText, List, InvoiceInfo, TableContainer, Table, Total, PaymentInfoContainer, FooterContainer, FooterRow, InvoiceTime, Medium, TableItem, InfoText, LightLink, Image } from "./style"
import useWindowDimensions from "@/hooks/use-window-dimensions"
import { TFunction } from "i18next"
import logo from "../../../public/assets/images/logo/logo-removebg-preview.png";
import { MailOutlined, PhoneOutlined, PrinterOutlined, ShopOutlined } from "@ant-design/icons";
import { Order } from "@/type/Order";
import { dateFormatter } from "@/utils/date";
import { getOrderStatus } from "@/utils/status";
import { OrderStatus } from "@/enums";
import { useRef } from "react";
import { Button } from "antd";
import { useReactToPrint } from "react-to-print";

type InvoiceProps = {
    t: TFunction<"translation", undefined>;
    order: Order | null;
}

export const Invoice: React.FC<InvoiceProps> = ({ t, order }) => {
    const { width } = useWindowDimensions();
    const contentRef = useRef<HTMLDivElement>(null);
    const reactToPrintFn = useReactToPrint({ contentRef });

    return (
        <>
            <Button type="default" icon={<PrinterOutlined />} onClick={() => reactToPrintFn()}>In hóa hơn</Button>

            <Section ref={contentRef}>
                <Container>
                    <LogoContainer>
                        <Image src={logo.src} alt="" />
                    </LogoContainer>

                    <InfoContainer>
                        <div>
                            <InvoiceText>Khách hàng</InvoiceText>
                            <h4>{order?.customerName}</h4>
                            <List>
                                <li><PhoneOutlined /> {order?.customerPhone}</li>
                                <li>info@xyzcompany.com</li>
                                <li>123 Main Street</li>
                            </List>
                        </div>
                        <div>
                            <InvoiceText>Điểm thu</InvoiceText>
                            <h4>Libra Restaurant</h4>
                            <List>
                                <li>Công ty TNHH một thành viên Libra</li>
                                <li>info@libra.com</li>
                                <li>52 Nguyễn Thị Minh Khai, Q1, HCM</li>
                            </List>
                        </div>
                    </InfoContainer>

                    <InvoiceInfo>
                        <Title>Thanh toán hóa đơn </Title>
                        <InvoiceTime>
                            <p className="m-0"> <Medium>Hóa đơn số:</Medium> #{order?.orderNo}</p>
                            <p className="m-0"> <Medium>Ngày thanh toán:</Medium> {dateFormatter(order?.latestStatusUpdate || new Date())}</p>
                            <p className="m-0">
                                <Medium >Trạng thái:</Medium>
                                <span style={{ color: getOrderStatus(order?.latestStatus || OrderStatus.Ready, t).color }}> {getOrderStatus(order?.latestStatus || OrderStatus.Ready, t).title}</span>
                            </p>
                        </InvoiceTime>
                    </InvoiceInfo>

                    <TableContainer>
                        <Table>
                            <thead>
                                <tr>
                                    <TableItem>STT</TableItem>
                                    <TableItem>Tên món</TableItem>
                                    <TableItem>Đơn giá</TableItem>
                                    <TableItem>Số lượng</TableItem>
                                    <TableItem>Tổng cộng</TableItem>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    order && order.orderLines && order.orderLines.length > 0 &&
                                    order.orderLines.map((item, index) => (
                                        <tr key={index}>
                                            <TableItem>{index + 1}</TableItem>
                                            <TableItem>{item?.foodName}</TableItem>
                                            <TableItem>{item?.foodPrice}</TableItem>
                                            <TableItem>{item?.quantity}</TableItem>
                                            <TableItem>{item?.foodPrice ? item?.foodPrice * item?.quantity : 0}</TableItem>
                                        </tr>
                                    ))
                                }
                                <tr>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <TableItem className="">Tạm tính</TableItem>
                                    <TableItem>{order?.subtotal}</TableItem>
                                </tr>
                                <tr>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <TableItem className="">Thuế</TableItem>
                                    <TableItem>0%</TableItem>
                                </tr>
                                <tr>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <TableItem></TableItem>
                                    <Total>Thành tiền</Total>
                                    <Total>{order?.total}</Total>
                                </tr>
                            </tbody>
                        </Table>
                    </TableContainer>
                    <PaymentInfoContainer>
                        <div>
                            <InfoText>Thông tin thanh toán</InfoText>
                            <ul className="list-unstyled">
                                <li><span className="fw-semibold">Số tài khoản: </span> 0349337045</li>
                                <li><span className="fw-semibold">Tên tài khoản: </span> LE MINH QUANG</li>
                                <li><span className="fw-semibold">Ngân hàng: </span> MB Bank </li>

                            </ul>
                        </div>

                        <div>
                            <InfoText>Thông tin liên hệ</InfoText>
                            <ul className="list-unstyled">
                                <li> <ShopOutlined /> Chi nhánh quận 1</li>
                                <li> <PhoneOutlined /> +84349337045</li>
                                <li> <MailOutlined /> minh.quang1720@gmail.com</li>
                            </ul>
                        </div>


                    </PaymentInfoContainer>

                    <div id="footer-bottom">
                        <FooterContainer>
                            <FooterRow>
                                <div className="copyright">
                                    <p>© 2024 Hóa đơn. <LightLink href="#">Điều khoản sử dụng</LightLink> </p>
                                </div>
                            </FooterRow>
                        </FooterContainer>
                    </div>
                </Container>
            </Section >
        </>
    )
}