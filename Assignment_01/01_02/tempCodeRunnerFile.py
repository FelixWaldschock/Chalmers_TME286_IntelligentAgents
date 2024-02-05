    for i in range(len(text)):
        if i < len(text) - 1 and text[i+1].strip()[0].isalpha():
            token = text[i]
            if input(f"Is '{token}' a part of an abbreviation? (y/n): ") == 'y':
                listOfAbbrevations.append(token)
        else:
            token = text[i]
            if input(f"Is '{token}' a part of an abbreviation? (y/n): ") == 'y':
                listOfAbbrevations.append(token)
    print(listOfAbbrevations)