import React from 'react';
import InputHeader from '@/components/input-header';
import { SpanLogo, Title, TitleLogo } from '@/components/title';
import BagIcon from '../../../public/assets/icons/bag-icon.svg';
import ScanIcon from '../../../public/assets/icons/qr-scan-icon.svg';
import {
  Container,
  IconContainer,
  LeftSideContainer,
  RightSideContainer,
  TitleContainer,
} from './styles';
import { theme } from 'twin.macro';
import useWindowDimensions from 'src/hooks/use-window-dimensions';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { TFunction } from 'i18next';

type HeaderProps = {
  t: TFunction<"translation", undefined>
}

export default function Header({ t }: HeaderProps) {
  const router = useRouter();

  const { width } = useWindowDimensions();
  return (
    <Container>
      <LeftSideContainer>
        <TitleContainer onClick={() => router.replace("/")}>
          <TitleLogo>Libra <SpanLogo>Restaurant</SpanLogo></TitleLogo>
        </TitleContainer>
      </LeftSideContainer>
      <RightSideContainer>
        <InputHeader t={t} />
      </RightSideContainer>
      {width < 1600 && (
        <IconContainer>
          <Link href='myorder' style={{ width: 22 }}><BagIcon fill={theme`textColor.primary`}></BagIcon></Link>
          <Link href='scan' style={{ width: 22 }}><ScanIcon fill={theme`textColor.primary`}></ScanIcon></Link>
        </IconContainer>
      )}
    </Container>
  );
}
