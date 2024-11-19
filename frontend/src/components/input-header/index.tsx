import React, { useCallback, useState } from 'react';
import { Container, ImageContainer, Input } from './styles';
import SearchIcon from '../../../public/assets/icons/search-icon.svg';
import { theme } from 'twin.macro';
import { TFunction } from 'i18next';
import { AutoComplete } from '../autocomplete';
import { useStoreDispatch, useStoreSelector } from '@/redux/store';
import { fetchData } from '@/redux/slices/products-slice';
import { debounce } from '@/utils/debounce';
import { toast } from 'react-toastify';
import { addItem } from '@/redux/slices/cart-slice';
import Item from '@/type/Item';

export default function InputHeader({ t }: { t: TFunction<"translation", undefined> }) {
  const [isOpen, setIsOpen] = useState(false);
  const { items, loading, id } = useStoreSelector(state => ({
    items: state.mainProductSlice.items,
    loading: state.mainProductSlice.loading,
    id: state.reservation.id
  }));

  const dispatch = useStoreDispatch();

  const debouncedDispatch = useCallback(
    debounce((value: string) => {
      dispatch(fetchData({ searchTerm: value }));
    }, 500),
    [dispatch]
  );

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.value.length > 0) {
      setIsOpen(true);

      debouncedDispatch(e.target.value);
    }
    else {
      setIsOpen(false);
    }
  }

  const handleChoose = (itemId: number) => {
    if (!id) {
      toast(t("you-have-not-reservation"), { type: "warning" });
    }
    else {
      const item = items.find(x => x.itemId === itemId);
      if (!item) {
        toast(t("food-not-exists"), { type: "error" });
      }
      else {
        dispatch(addItem({ item }));
      }
    }
  }

  return (
    <Container>
      <ImageContainer>
        <SearchIcon fill={theme`textColor.primary`} />
      </ImageContainer>
      <Input placeholder={t("search-food")} onChange={handleChange} />

      <AutoComplete
        isOpen={isOpen}
        loading={loading}
        items={items.map(item => ({ label: item.title, value: item.itemId }))}
        handleChoose={handleChoose}
      />
    </Container>
  );
}
