import requests
from ics import Calendar

faglinks = {"Data 2. Ã¥r": "https://cloud.timeedit.net/uia/web/tp/ri15667y6Z0655Q097QQY656Z067057Q469W95.ics"}

def get_data():
    for name in faglinks.keys():
        url = faglinks[name]
        response = requests.get(url, allow_redirects=True).text
        cal = Calendar(response)

        for event in cal.events:
            fag = event.name
            if "lab" in fag.lower():
                rom = process_rom(event.location)
                foreleser = process_foreleser(event.description)
                date = event.begin.date()
                begin = event.begin.time()
                end = event.end.time()

                print("-----------------------------------")
                print("Fag: {}".format(fag))
                print("Rom: {}".format(rom))
                print("Dato: {}".format(date))
                print("Start: {}".format(begin))
                print("Slutt: {}".format(end))
                print("Foreleser: {}".format(foreleser))


def process_foreleser(text):
    if "\n" in text:
        return text.split('\n')[0]
    else:
        return text

def process_rom(text):
    if "," in text:
        return text.split(",")
    elif "/" in text:
        loc = text.split(" ")[0]
        dep = text.split(" ")[1]
        rooms = text.split("/")
        return [[loc + " " + dep + " " + x for x in rooms]]
    else:
        return [text]

get_data()