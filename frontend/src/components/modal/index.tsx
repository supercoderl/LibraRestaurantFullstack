
import useWindowDimensions from "@/hooks/use-window-dimensions";
import React from "react";
import Modal from 'react-modal';

interface ModalProps {
    onOpenChange: () => void;
    open: boolean;
    children?: React.ReactNode;
    isTransparent?: boolean;
}

export const ModalSection: React.FC<ModalProps> = ({ onOpenChange, open, children, isTransparent }) => {
    const { width } = useWindowDimensions();
    const customStyles = {
        content: {
            top: '50%',
            left: '50%',
            right: 'auto',
            bottom: 'auto',
            marginRight: '-50%',
            transform: 'translate(-50%, -50%)',
            backgroundColor: isTransparent ? 'transparent' : 'white',
            textAlign: 'center',
            border: isTransparent && 'none',
            width: width <= 767 && "95%"
        },
        overlay: {
            backgroundColor: 'rgba(0, 0, 0, 0.4)',
            zIndex: 20
        }
    };

    return (
        <Modal
            isOpen={open}
            onRequestClose={onOpenChange}
            style={customStyles as Modal.Styles}
            contentLabel="Example Modal"
        >
            {children}
        </Modal>
    )
}